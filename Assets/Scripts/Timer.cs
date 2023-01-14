using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timeToCompleteQuestion = 30f;
    [SerializeField] private float timeToShowCorrectAnswer = 10f;

    public bool isAnsweringQuestion = false;

    private float timerValue;

    void Update()
    {
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        timerValue -= Time.deltaTime;

        // Case guard
        switch (timerValue)
        {
            case <= 0 when !isAnsweringQuestion:
                timerValue = timeToCompleteQuestion;
                isAnsweringQuestion = true;
                break;
            case <= 0 when isAnsweringQuestion:
                timerValue = timeToShowCorrectAnswer;
                isAnsweringQuestion = false;
                break;
        }


    }
}
