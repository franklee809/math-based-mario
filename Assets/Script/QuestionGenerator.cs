using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionGenerator : MonoBehaviour
{
    public int plusnum1, plusnum2;
    public int minusnum1, minusnum2;
    public int multinum1, multinum2;
    public int divnum1, divnum2;
    public int sumplus, summinus, summulti, sumdiv;
    public int correctAns;
    public string one, two, three, four;
    public string temp, type;
    public int currentQuestionIndex = 1;
    public Text questext = null;
    //public TextMesh Opt11;
    public Text result = null;
    private TextMesh testMesh;


    void Start()
    {
        plusequation();

    }

    public void plusequation()
    {
        plusnum1 = Random.Range(0, 50);
        plusnum2 = Random.Range(0, 50);
        questext.text = plusnum1.ToString() + " + " + plusnum2.ToString() + " = ?";
        sumplus = plusnum1 + plusnum2;
        one = Random.Range(0, 100).ToString();
        two = Random.Range(0, 100).ToString();
        three = Random.Range(0, 100).ToString();
        four = Random.Range(0, 100).ToString();
        string[] optionarray = { one, two, three, four };
        int optindex = Random.Range(0, 4);

        if (currentQuestionIndex == 0) { int currentQuestionIndex = 1; }
        string answer;
        for (int i = 0; i < 4; i++)
        {
            if (i != optindex)
            {
                if (optionarray[i] == sumplus.ToString())
                {
                    while (optionarray[i] == sumplus.ToString())
                    {
                        optionarray[i] += 1;
                    }
                    answer = optionarray[i];
                    }
                else
                {
                     answer = optionarray[i];
                  }
            }
           else
            {
                 answer = sumplus.ToString();
                 correctAns = sumplus;
            }

            temp = string.Concat(string.Concat("Cube", currentQuestionIndex), "/GameObject");
            GameObject.Find(temp).GetComponent<TextMesh>().text = answer;
            currentQuestionIndex += 1;
        }
        type = "sum"; 
    }

    public void minusequation()
    {
        minusnum1 = Random.Range(1, 100);
        minusnum2 = Random.Range(1, minusnum1);
        questext.text = minusnum1.ToString() + " - " + minusnum2.ToString() + " = ?";
        summinus = minusnum1 - minusnum2;
        one = Random.Range(0, 100).ToString();
        two = Random.Range(0, 100).ToString();
        three = Random.Range(0, 100).ToString();
        four = Random.Range(0, 100).ToString();
        string[] optionarray = { one, two, three, four };
        int optindex = Random.Range(0, 4);

        if (currentQuestionIndex == 0) { int currentQuestionIndex = 1; }

        string answer;
        for (int i = 0; i < 4; i++)
        {
            if (i != optindex)
            {
                if (optionarray[i] == summinus.ToString())
                {
                    while (optionarray[i] == summinus.ToString())
                    {
                        optionarray[i] += 1;
                    }
                    answer = optionarray[i];
                    }
                else
                {
                    answer = optionarray[i];
                }
            }
            else
            {
                answer = summinus.ToString();
                correctAns = summinus;
            }
            temp = string.Concat(string.Concat("Cube", currentQuestionIndex), "/GameObject");
            GameObject.Find(temp).GetComponent<TextMesh>().text = answer;
            currentQuestionIndex += 1;
        }
        type = "minus";
    }

    public void multiequation()
    {
        multinum1 = Random.Range(2, 12);
        multinum2 = Random.Range(2, 12);
        questext.text = multinum1.ToString() + " * " + multinum2.ToString() + " = ?";
        summulti = multinum1 * multinum2;
        one = Random.Range(0, 100).ToString();
        two = Random.Range(0, 100).ToString();
        three = Random.Range(0, 100).ToString();
        four = Random.Range(0, 100).ToString();
        string[] optionarray = { one, two, three, four };
        int optindex = Random.Range(0, 4);


        if (currentQuestionIndex == 0) { int currentQuestionIndex = 1; }

        string answer;
        for (int i = 0; i < 4; i++)
        {
            if (i != optindex)
            {
                if (optionarray[i] == summulti.ToString())
                {
                    while(optionarray[i] == summulti.ToString())
                    {
                        optionarray[i] += 1;
                        }
                    answer = optionarray[i];
                    }
                else
                {
                    answer = optionarray[i];
                }
            }
            else
            {
                answer = summulti.ToString();
                correctAns = summulti;
            }
            temp = string.Concat(string.Concat("Cube", currentQuestionIndex), "/GameObject");
            GameObject.Find(temp).GetComponent<TextMesh>().text = answer;
            currentQuestionIndex += 1;
        }
        type = "mul";
    }

    public void divequation()
    {
        divnum2 = Random.Range(2, 11);
        int temp1 = Random.Range(2, 11);
        divnum1 = divnum2 * temp1;
        questext.text = divnum1.ToString() + " / " + divnum2.ToString() + " = ?";
        sumdiv = divnum1 / divnum2;
        one = Random.Range(0, 100).ToString();
        two = Random.Range(0, 100).ToString();
        three = Random.Range(0, 100).ToString();
        four = Random.Range(0, 100).ToString();
        string[] optionarray = { one, two, three, four };
        int optindex = Random.Range(0, 4);


        if (currentQuestionIndex == 0) { int currentQuestionIndex = 1; }
        string answer;
        for (int i = 0; i < 4; i++)
        {
            if (i != optindex)
            {
                if (optionarray[i] == sumdiv.ToString())
                {
                    while (optionarray[i] == sumdiv.ToString())
                    {
                        optionarray[i] += 1;
                    }
                    answer = optionarray[i];
                    }
                else
                {
                    answer = optionarray[i];
                }
            }
            else
            {
                answer = sumdiv.ToString();
                correctAns = sumdiv;
            }
            temp = string.Concat(string.Concat("Cube", currentQuestionIndex), "/GameObject");
            GameObject.Find(temp).GetComponent<TextMesh>().text = answer;
            currentQuestionIndex += 1;
        }
        type = "div";
    }

    public void ResetQuestionBlock()
    {
        int temp1 = currentQuestionIndex-1;
        for(int i=0; i<4; i++)
        {
            temp = string.Concat(string.Concat("Cube", temp1), "/GameObject");
            GameObject.Find(temp).GetComponent<TextMesh>().text = "?";
            temp1 -= 1;
        }
    }
}
