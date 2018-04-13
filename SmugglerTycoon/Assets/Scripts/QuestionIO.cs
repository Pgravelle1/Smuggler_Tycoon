using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionIO
{

    public string question;
    public string responseOne;
    public int trustValueOne;
    public string responseTwo;
    public int trustValueTwo;
    public string responseThree;
    public int trustValueThree;
    public string responseFour;
    public int trustValueFour;

    public void QuestionForm(string inputQuestion, string inputResponseOne, int inputTrustOne,
                                                   string inputResponseTwo, int inputTrustTwo,
                                                   string inputResponseThree, int inputTrustThree,
                                                   string inputResponseFour, int inputTrustFour)
    {
        question = inputQuestion;
        responseOne = inputResponseOne;
        trustValueOne = inputTrustOne;
        responseTwo = inputResponseTwo;
        trustValueTwo = inputTrustTwo;
        responseThree = inputResponseThree;
        trustValueThree = inputTrustThree;
        responseFour = inputResponseFour;
        trustValueFour = inputTrustFour;

    }

}
