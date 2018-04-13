using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    static public float CurrentTimeScale = 1f;
    roadBlockQuestions roadBlockQuestions;
    private void Start()
    {
        roadBlockQuestions = GameObject.FindGameObjectWithTag("MissionController").GetComponent<roadBlockQuestions>();
    }

    public void FastTime()
    {
        if (!roadBlockQuestions.showGUI)
        {
            Time.timeScale = 10;
            CurrentTimeScale = 10;
        }
    }

    public void NormalTime()
    {
        if (!roadBlockQuestions.showGUI)
        {
            Time.timeScale = 1;
            CurrentTimeScale = 1;
        }
    }

}
