using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Transform leftBounds;
    public Transform rightBounds;

    private float camWidth, camHeight, levelMinX, levelMaxX, initialY = 7.5f;

    private Vector3 smoothDampVelocity = Vector3.zero, offset;

    private float smoothDampTime = 0.15f;

    // Make sure the camera is targeting players.
    void Start()
    {
        camHeight = Camera.main.orthographicSize * 2;
        camWidth = camHeight * Camera.main.aspect;

        float leftBoundsWidth = leftBounds.GetComponentInChildren<SpriteRenderer>().bounds.size.x / 2;
        float rightBoundsWidth = rightBounds.GetComponentInChildren<SpriteRenderer>().bounds.size.x / 2;

        levelMinX = leftBounds.position.x + leftBoundsWidth + (camWidth / 2);
        levelMaxX = rightBounds.position.x - rightBoundsWidth - (camWidth / 2);

        offset = transform.position - target.transform.position;


    }


    // Update is called once per frame
    void Update()
    {

        if(target.transform.position.y > 13.5f)
        {
            float targetX = Mathf.Max(levelMinX, Mathf.Min(levelMaxX, target.position.x));

            float x = Mathf.SmoothDamp(transform.position.x, targetX, ref smoothDampVelocity.x, smoothDampTime);
            //float y = Mathf.SmoothDamp(transform.position.y, , ref smoothDampVelocity.x, smoothDampTime);

            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }



        if (target)
        {
            // Script 1 
            float targetX = Mathf.Max(levelMinX, Mathf.Min(levelMaxX, target.position.x));

            float x = Mathf.SmoothDamp(transform.position.x, targetX, ref smoothDampVelocity.x, smoothDampTime);

            transform.position = new Vector3(x, initialY, transform.position.z);

            // Script 2
            //transform.position = target.transform.position + offset;
        }

    }
}
