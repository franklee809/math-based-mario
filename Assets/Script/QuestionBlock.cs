using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBlock : MonoBehaviour
{
    /**
    * @ 
    
    */

    public float bounceHeight = 0.5f;
    public float bounceSpeed = 4f;

    private Vector2 originalPosition;

    public bool canBounce = false; 

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    public void QuestionBlockBounce() {
        string a = GetComponentInChildren<TextMesh>().text;

        if (canBounce & a != "?") {
            canBounce = false;
            StartCoroutine(Bounce());
            canBounce = true;
        }
    }
    void Update()
    {
        if (canBounce == false)
        {
            CheckBool();
        
        }
    }

    IEnumerator Bounce() {

        while (true) {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + bounceSpeed * Time.deltaTime);
            //Debug.Log("Start "+transform.localPosition.y);
            if (transform.localPosition.y >= originalPosition.y + bounceHeight)
                break;
            yield return null; 
        }

        while (true) {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - bounceSpeed * Time.deltaTime);
            //Debug.Log("End "+ transform.localPosition.y);
            if (transform.localPosition.y <= originalPosition.y)
            {
                transform.localPosition = originalPosition;
                break;
            }
            yield return null;
        }
    }

    public void CheckBool()
    {
        string a = GetComponentInChildren<TextMesh> ().text;

        if ( a != "?")
        {
            canBounce = true;
        }
    }
}
