using UnityEngine;

public class QuestionsManager : MonoBehaviour
{
    public QuestionData[] Data;

    [SerializeField] private QuestionView questionView;

    private int _currentQuestion;
    
    private void Start()
    {
        questionView.NextQuestionButtonClicked += OnNextQuestionButtonClicked;

        questionView.Set(Data[0]);
    }

    private void OnNextQuestionButtonClicked()
    {
        _currentQuestion += 1;

        questionView.Set(Data[_currentQuestion]);
    }
}

[System.Serializable]
public struct QuestionData
{
    public QuestionType Type;
    public string Question;
    public AnswersData[] Answers;
}

[System.Serializable]
public struct AnswersData
{
    public string Text;
    public Sprite Image;
}

public enum QuestionType
{ 
    DragAndDrop,
    Background
}