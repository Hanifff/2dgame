using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteQuestion = 10f;
    [SerializeField] float timeToShowCorrectQuestion = 3f;
    public bool loadNxtQst = false;
    public bool isAnsweringQuestion = true;
    public float fillFraction;
    float timerValue;
    void Update()
    {
        UpdateTimer();
    }
    public void CancelTimer()
    {
        timerValue = 0;
    }
    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;
        if (isAnsweringQuestion)
        {
            if (timerValue > 0)
            {
                fillFraction = timerValue / timeToCompleteQuestion;
            }
            else
            {
                Debug.Log("not answering 1");
                isAnsweringQuestion = false;
                timerValue = timeToShowCorrectQuestion;
            }
        }
        else
        {
            if (timerValue > 0)
            {
                fillFraction = timerValue / timeToCompleteQuestion;
            }
            else
            {
                Debug.Log("has time? " + timerValue);
                Debug.Log("not answering 2");
                isAnsweringQuestion = false;
                timerValue = timeToCompleteQuestion;
                loadNxtQst = true;
            }
        }
    }
}
