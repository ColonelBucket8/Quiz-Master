using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timeToCompleteQuestion = 30f;
    [SerializeField] private float timeToShowCorrectAnswer = 10f;

    public bool loadNextQuestion;
    public bool isAnsweringQuestion = false;
    public float fillFraction;

    private float timerValue;

    void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer()
    {
        timerValue = 0;
    }

    private void UpdateTimer()
    {
        timerValue -= Time.deltaTime;

        // Case guard
        switch (timerValue)
        {
            case > 0 when isAnsweringQuestion:
                fillFraction = timerValue / timeToCompleteQuestion;
                break;
            case <= 0 when isAnsweringQuestion:
                timerValue = timeToShowCorrectAnswer;
                isAnsweringQuestion = false;
                break;

            case > 0 when !isAnsweringQuestion:
                fillFraction = timerValue / timeToShowCorrectAnswer;
                break;
            case <= 0 when !isAnsweringQuestion:
                timerValue = timeToCompleteQuestion;
                isAnsweringQuestion = true;
                loadNextQuestion = true;
                break;


        }


    }
}
