using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    Quiz quiz;
    Win win;
    void Awake()
    {
        quiz = FindObjectOfType<Quiz>();
        win = FindObjectOfType<Win>();
    }
    void Start()
    {
        quiz.gameObject.SetActive(true);
        win.gameObject.SetActive(false);
    }
    void Update()
    {
        if (quiz.isComplete)
        {
            quiz.gameObject.SetActive(false);
            win.gameObject.SetActive(true);
            win.ShowFinalScore();

        }
    }
    public void OnrReplyLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
