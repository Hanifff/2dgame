using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteQuestion = 30f;
    [SerializeField] float timeToShowCorrectQuestion = 10f;
    float timerValue;
    void Update()
    {

    }
    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;
    }
}
