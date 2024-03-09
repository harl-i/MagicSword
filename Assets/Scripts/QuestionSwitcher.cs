using UnityEngine;

public class QuestionSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject _questionFirst;
    [SerializeField] private GameObject _questionSecond;

    public void FirstQuestionEnable()
    {
        _questionFirst.SetActive(true);
    }

    public void SecondQuestionEnable()
    {
        _questionSecond.SetActive(true);
    }

    public void DisableQuestions()
    {
        _questionFirst.SetActive(false);
        _questionSecond.SetActive(false);
    }
}
