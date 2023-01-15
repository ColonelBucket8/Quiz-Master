using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")] [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private List<QuestionSO> questions = new();

    [Header("Answers")] [SerializeField] private GameObject[] answerButtons;

    [Header("Button Colors")] [SerializeField]
    private Sprite defaultAnswerSprite;

    [SerializeField] private Sprite correctAnswerSprite;

    [Header("Timer")] [SerializeField] private Image timerImage;


    private Image buttonImage;
    private int correctAnswerIndex;
    private QuestionSO currentQuestion;
    private bool hasAnsweredEarly;
    private Timer timer;


    private void Start()
    {
        timer = FindObjectOfType<Timer>();
    }

    private void Update()
    {
        timerImage.fillAmount = timer.fillFraction;

        if (timer.loadNextQuestion)
        {
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    private void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();

        for (var i = 0; i < answerButtons.Length; i++)
        {
            var buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    private void GetNextQuestion()
    {
        if (questions.Count > 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestion();
        }
    }

    private void GetRandomQuestion()
    {
        var index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];

        if (questions.Contains(currentQuestion)) questions.Remove(currentQuestion);
    }

    private void SetDefaultButtonSprites()
    {
        foreach (var answerButton in answerButtons)
        {
            buttonImage = answerButton.GetComponentInParent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }

    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
    }

    private void DisplayAnswer(int index)
    {
        if (index == currentQuestion.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct!";
            buttonImage = answerButtons[index].GetComponentInChildren<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
        else
        {
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            var correctAnswerText = currentQuestion.GetAnswer(correctAnswerIndex);

            questionText.text = "Sorry, the correct answer was;\n" + correctAnswerText;

            buttonImage = answerButtons[correctAnswerIndex].GetComponentInChildren<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    private void SetButtonState(bool state)
    {
        foreach (var answerButton in answerButtons)
        {
            var button = answerButton.GetComponent<Button>();
            button.interactable = state;
        }
    }
}