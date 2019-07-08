using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderChanger : MonoBehaviour
{
    public Slider slider;
    public RespawnPoint respawn;

    public float health,damage,period =0.1f;
    private float nextPeriodTime;
        
    

    void Update()
    {
        if (System.Math.Abs(health) > 100)
        {
            health = 100;
        }
        if (System.Math.Abs(health) <= 0)
        {
            die();
        }
        if (Time.time >= nextPeriodTime)
        {
            health -= damage;
            slider.value = health;
            nextPeriodTime += period;

        }
    }

    public void WrongAnswer()
    {
        health = System.Math.Abs(health) - (System.Math.Abs(health * 0.1f));
    }

    public void TrueAnswer()
    {
        health = System.Math.Abs(health) + (System.Math.Abs(health * 0.1f));
    }

    public void ReduceHealth()
    {
        health = System.Math.Abs(health) - (System.Math.Abs(health * 0.2f));

        }
    public void die()
    {
        //int respawnVal = GameObject.Find("Respawn1").GetComponent<"">
            respawn.Respawn();
            Debug.Log("success");
    }
}
