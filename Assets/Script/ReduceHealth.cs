using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceHealth : MonoBehaviour
{
    public SliderChanger slide;
    void Start()
    {

    }

    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            //col.transform.position = SpawnPoint.position;
            //Player.transform.position = new Vector3(5, 5, 0);
            slide.ReduceHealth();


        }
    }
}
