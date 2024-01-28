using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionView : MonoBehaviour
{
    public event Action NextQuestionButtonClicked;

    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private ToggleWithAdditionalGraphics answerButtonTemplate;
    [Space]
    [SerializeField] private Transform buttonsRoot;
    [SerializeField] private ToggleGroup toggleGroup;
    [Space]
    [SerializeField] private Button nextQuestionButton;
    [Header("DragAndDrop")]
    [SerializeField] private WishPart wishPartTemplate;
    [SerializeField] private Transform wishPartFrame;
    [Header("Background")]
    [SerializeField] private Image background;

    private WishPart _currentWishPart;

    //private Vector2 _basePosition;

    private void Awake()
    {
        nextQuestionButton.onClick.AddListener(() => NextQuestionButtonClicked?.Invoke());

        //_basePosition = transform.position;
    }

    public void Set(QuestionData data)
    {
        //transform.DOMove(_basePosition, 0.45f).From(_basePosition - Vector2.right * Screen.width);

        questionText.text = data.Question;

        foreach (Transform child in buttonsRoot)
            Destroy(child.gameObject);

        wishPartFrame.gameObject.SetActive(data.Type == QuestionType.DragAndDrop);

        if (data.Type == QuestionType.DragAndDrop)
        {
            _currentWishPart = Instantiate(wishPartTemplate, wishPartFrame);
        }

        foreach (var answer in data.Answers)
        {
            var newButton = Instantiate(answerButtonTemplate, buttonsRoot);

            newButton.SetText(answer.Text);
            newButton.onValueChanged.AddListener(x =>
            {
                if (data.Type == QuestionType.Background)
                {
                    background.sprite = answer.Image;
                }

                if (data.Type == QuestionType.DragAndDrop)
                {
                    _currentWishPart.Set(answer.Image, wishPartFrame.position);
                }
            });
        }
    }
}