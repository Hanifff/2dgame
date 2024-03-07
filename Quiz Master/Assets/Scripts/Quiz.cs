using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionTxt;
    [SerializeField] QuestionSO question;
    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIdx;
    [Header("Buttons")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    void Start()
    {
        timer = FindObjectOfType<Timer>();
        GetNextQuestion();
    }
    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
    }
    public void OnAnswerSelected(int idx)
    {
        Image btnImage;
        if (idx == question.GetCorrectAnswerIdx())
        {
            questionTxt.text = "Correct!";
            btnImage = answerButtons[idx].GetComponent<Image>();
            btnImage.sprite = correctAnswerSprite;
        }
        else
        {
            correctAnswerIdx = question.GetCorrectAnswerIdx();
            string correctAnswer = question.GetAnswer(correctAnswerIdx);
            questionTxt.text = "Sorry the correct answer was\n" + correctAnswer;
            btnImage = answerButtons[correctAnswerIdx].GetComponent<Image>();
            btnImage.sprite = correctAnswerSprite;
        }
        SetButtonState(false);
    }

    void GetNextQuestion()
    {
        SetButtonState(true);
        SetDefaultButtonSprite();
        DisplayQuestion();
    }

    void DisplayQuestion()
    {
        questionTxt.text = question.GetQuestion();
        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI btnTxt = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            btnTxt.text = question.GetAnswer(i);
        }
    }

    void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }
    void SetDefaultButtonSprite()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image btnImage = answerButtons[i].GetComponent<Image>();
            btnImage.sprite = defaultAnswerSprite;
        }
    }

}
