using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Castle : MonoBehaviour
{
    public string stage;
    /**
    * @ onTriggerEnter2D- When Player characters touched the castle
    */

    private void OnTriggerEnter2D(Collider2D collision) {
        
        if (collision.tag == "Player") {
            SceneManager.LoadScene(stage);
        }
    }

}
