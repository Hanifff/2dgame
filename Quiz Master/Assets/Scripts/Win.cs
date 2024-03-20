using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Xml.Serialization;

public class Win : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreTxt;
    ScoreKeeper scoreKeeper;
    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    public void ShowFinalScore()
    {
        finalScoreTxt.text = "Congralutations!\nYou got a score of " +
         scoreKeeper.CalculateScore() + "%";
    }
}
