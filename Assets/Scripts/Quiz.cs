using TMPro;
using UnityEngine;

public class Quiz : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] QuestionSO question;

    void Start()
    {
        questionText.text = question.GetQuestion();
    }

}
