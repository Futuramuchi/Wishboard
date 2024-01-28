using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private Button startButtonClicked;
    [Space]
    [SerializeField] private Questions questions;

    private void Start()
    {
        startButtonClicked.onClick.AddListener(Hide);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        questions.gameObject.SetActive(true);
    }
}