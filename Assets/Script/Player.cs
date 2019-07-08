﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpVelocity,bounceVelocity;  
    public Vector2 velocity;
    public float gravity;
    public LayerMask wallMask,floorMask;
    public SliderChanger slideChange;
    public QuestionGenerator generator;
    public RespawnPoint respawn;

    private bool walk, walk_left, walk_right, jump, jump_with_up;

    // const name   
    public enum PlayerState{
        jumping,
        idle,
        walking,
        bouncing
    }

    private PlayerState playerState = PlayerState.idle;

    private bool grounded = false;
    private bool bounce = false; 
        
    void Start() {
        //Fall();
    }

    void Update() {
        CheckPlayerInput();
        UpdatePlayerPosition();
        UpdateAnimationStates();
        DropGround();
    }

    // Player movement
    void UpdatePlayerPosition() {
        Vector3 pos = transform.localPosition;
        Vector3 scale = transform.localScale;
        if (walk) {
            if (walk_left) {
                pos.x -= velocity.x * Time.deltaTime;
                scale.x = -1;
            }
            if (walk_right) {
                pos.x += velocity.x * Time.deltaTime;
                scale.x = 1;
            }
            pos = CheckWallRays(pos, scale.x);

        }

        if (jump && playerState != PlayerState.jumping) {
            playerState = PlayerState.jumping;
            velocity = new Vector2(velocity.x, jumpVelocity);

        }

        if (playerState == PlayerState.jumping) { 
            pos.y += velocity.y * Time.deltaTime;
            velocity.y -= gravity * Time.deltaTime;
        }

        if(bounce && playerState!= PlayerState.bouncing) {
            playerState = PlayerState.bouncing;
            velocity = new Vector2(velocity.x, bounceVelocity);  
        }
          
        if (playerState == PlayerState.bouncing) {
            pos.y += velocity.y * Time.deltaTime;
            velocity.y -= gravity * Time.deltaTime;
        }

        if(velocity.y <= 0) {
            pos = CheckFloorRays(pos); 
        }
        if (velocity.y >= 0) {
            pos = CheckCeilingRays(pos);
        }
           
        transform.localPosition = pos;
        transform.localScale = scale;   
    }


    // Add in players animations
    void UpdateAnimationStates() {
        if (grounded & !walk && !bounce){
            GetComponent<Animator>().SetBool("isJumping", false);
            GetComponent<Animator>().SetBool("isRunning", false);
        }


        if (grounded && walk ) {
            GetComponent<Animator>().SetBool("isJumping", false);
            GetComponent<Animator>().SetBool("isRunning", true);
        }

        if (playerState == PlayerState.jumping) {
            GetComponent<Animator>().SetBool("isJumping", true);
            GetComponent<Animator>().SetBool("isRunning", false);
        }
    }



    // Check to ensure players key binding is correct
    void CheckPlayerInput() {
        bool input_left = Input.GetKey(KeyCode.LeftArrow);
        bool input_right = Input.GetKey(KeyCode.RightArrow);
        bool input_space = Input.GetKeyDown(KeyCode.Space);
        bool input_up = Input.GetKey(KeyCode.UpArrow);

        walk = input_left || input_right;
        walk_left = input_left & !input_right;
        walk_right = !input_left & input_right;
        jump = input_space || input_up;
    }
    
    // Collision part
    Vector3 CheckWallRays(Vector3 pos,float direction){

        Vector2 originTop = new Vector2(pos.x + direction * .4f, pos.y + 1f - 0.2f);
        Vector2 originMiddle = new Vector2(pos.x + direction * .4f, pos.y );
        Vector2 originBottom = new Vector2(pos.x + direction * .4f, pos.y - 1f + 0.2f);

        RaycastHit2D wallTop = Physics2D.Raycast(originTop, new Vector2(direction, 0), velocity.x * Time.deltaTime, wallMask);
        RaycastHit2D wallMiddle = Physics2D.Raycast(originMiddle, new Vector2(direction, 0), velocity.x * Time.deltaTime, wallMask);
        RaycastHit2D wallBottom = Physics2D.Raycast(originBottom, new Vector2(direction, 0), velocity.x * Time.deltaTime, wallMask);

        if(wallTop.collider != null || wallMiddle.collider!= null || wallBottom.collider != null){
            pos.x -= velocity.x * Time.deltaTime * direction; 
        }

        return pos; 
    }

    // Let Player won't go through floor part
    Vector3 CheckFloorRays(Vector3 pos) {

        Vector2 originLeft = new Vector2(pos.x - 0.5f + 0.2f, pos.y -1f);
        Vector2 originMiddle = new Vector2(pos.x, pos.y - 1f);
        Vector2 originRight = new Vector2(pos.x + 0.5f - 0.2f, pos.y - 1f);

        RaycastHit2D floorLeft = Physics2D.Raycast(originLeft, Vector2.down, velocity.y * Time.deltaTime, floorMask);
        RaycastHit2D floorMiddle = Physics2D.Raycast(originMiddle, Vector2.down, velocity.y * Time.deltaTime, floorMask);
        RaycastHit2D floorRight = Physics2D.Raycast(originRight, Vector2.down, velocity.y * Time.deltaTime, floorMask);

        if (floorLeft.collider != null || floorMiddle.collider != null || floorRight.collider != null) {

            RaycastHit2D hitRay = floorRight;

            if (floorRight) {
                hitRay = floorLeft;
            } else if (floorMiddle) {
                hitRay = floorMiddle;
            } else if(floorRight) {
                hitRay = floorRight; 
            }
            /* Error occurs at this line
            if (hitRay.collider.tag == "Enemy") {
                bounce = true;
                hitRay.collider.GetComponent<Enemy>().Crush();
            }*/
            playerState = PlayerState.idle;
            grounded = true;
            velocity.y = 0;

            try {
                pos.y = hitRay.collider.bounds.center.y + hitRay.collider.bounds.size.y / 2 + 1;
            }
            catch (System.NullReferenceException) {
              // Debug.Log("Pos was not set in the inspector");
            }


        }
        else {
       
            if (playerState != PlayerState.jumping) {
                Fall();
            }
        }
        return pos;
    }

    // Let player cannot go through Ceiling 
    Vector3 CheckCeilingRays(Vector3 pos) {
        Vector2 originLeft = new Vector2(pos.x - 0.5f + 0.2f, pos.y + 1f);
        Vector2 originMiddle = new Vector2(pos.x, pos.y + 1f);
        Vector2 originRight = new Vector2(pos.x + 0.5f - 0.2f, pos.y + 1f);

        RaycastHit2D ceilLeft = Physics2D.Raycast(originLeft, Vector2.up, velocity.y * Time.deltaTime, floorMask);
        RaycastHit2D ceilMiddle = Physics2D.Raycast(originMiddle, Vector2.up, velocity.y * Time.deltaTime, floorMask);
        RaycastHit2D ceilRight = Physics2D.Raycast(originRight, Vector2.up, velocity.y * Time.deltaTime, floorMask);

        if (ceilLeft.collider != null || ceilMiddle.collider != null || ceilRight.collider != null) {

            RaycastHit2D hitRay = ceilLeft;

            if (ceilLeft) {
                hitRay = ceilLeft;
            }
            else if (ceilMiddle) { 
                hitRay = ceilMiddle;
            }
            else if (ceilRight) {
                hitRay = ceilRight;
            }

            if (hitRay.collider.tag == "QuestionBlock") {
                try{
                    hitRay.collider.GetComponent<QuestionBlock>().QuestionBlockBounce();
                    Check(hitRay.collider.gameObject.name);

                }
                catch (System.NullReferenceException) {
                    Debug.Log("Null");
                }
            }
 
            pos.y = hitRay.collider.bounds.center.y - hitRay.collider.bounds.size.y / 2 - 1;

            Fall();
        }
        return pos; 

    }


    // Let player falls down after jumping 

    void Fall(){
        velocity.y = 0;

        playerState = PlayerState.jumping;
        bounce = false;
        grounded = false;

    }

    void Check(string name1)
    {

        string val = GameObject.Find(name1).GetComponentInChildren<TextMesh>().text;
        //Debug.Log("Name :" + val);
        string correctAns = GameObject.Find("Question").GetComponent<QuestionGenerator>().correctAns.ToString();
        //Debug.Log("correctAns :" + correctAns);

            if (correctAns == val)
            {
                slideChange.TrueAnswer();
                generator.ResetQuestionBlock();
                RandomFunction();
            }

            else if(correctAns != val && val != "?")
            {
                slideChange.WrongAnswer();
            }

            else
            {
                Debug.Log("failed");
            }

    }

    void RandomFunction()
    {
        int rand = Random.Range(0, 4);

        switch (rand)
        {
            case 0:
                generator.plusequation();
                break;
            case 1:
                generator.minusequation();
                break;
            case 2:
                generator.multiequation();
                break;
            case 3:
                generator.divequation();
                break;
            default:break;
        }

    }
    public void DropGround()
    {
        Vector3 tran = transform.position;
        if (System.Math.Round(tran.y) <= -2f)
        {
            respawn.Respawn();
        }
    }
}

