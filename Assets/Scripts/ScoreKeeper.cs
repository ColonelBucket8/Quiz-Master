using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    // Default int without value initialization is 0
    private int correctAnswers;

    private int questionsSeen;

    public int GetCorrectAnswers()
    {
        return correctAnswers;
    }

    public void IncrementCorrectAnswers()
    {
        correctAnswers++;
    }

    public int GetQuestionsSeen()
    {
        return questionsSeen;
    }

    public void IncrementQuestionsSeen()
    {
        questionsSeen++;
    }

    public int CalculateScore()
    {
        float correctAnswersFloat = correctAnswers / (float)questionsSeen * 100;
        return Mathf.RoundToInt(correctAnswersFloat);
    }
}