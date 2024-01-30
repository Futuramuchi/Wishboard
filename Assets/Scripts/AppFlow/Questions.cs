using UnityEngine;

public class Questions : MonoBehaviour
{
    public QuestionData[] Data;

    [SerializeField] private QuestionView questionView;
    [Space]
    [SerializeField] private FinalScreen finalScreen;

    private int _currentQuestion;
    
    private void Start()
    {
        questionView.NextQuestionButtonClicked += OnNextQuestionButtonClicked;

        questionView.Set(Data[0], $"До отправки на реализацию осталось шагов: {Data.Length - _currentQuestion}");
    }

    private void OnNextQuestionButtonClicked()
    {
        if (_currentQuestion == Data.Length - 1)
        {
            finalScreen.gameObject.SetActive(true);
        }

        _currentQuestion += 1;

        questionView.Set(Data[_currentQuestion], $"До отправки на реализацию осталось шагов: {Data.Length - _currentQuestion}");
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
    public AudioClip AudioClip;
}

public enum QuestionType
{ 
    DragAndDrop,
    Background
}