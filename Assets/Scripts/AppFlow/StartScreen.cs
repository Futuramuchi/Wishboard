using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private Questions questions;
    [Space]
    [Header("Login")]
    [SerializeField] private Button loginButton;
    [SerializeField] private GameObject loginView;
    [SerializeField] private TextMeshProUGUI hintText;
    [SerializeField] private TMP_InputField inputField;
    [Header("Start")]
    [SerializeField] private Button startButton;
    [SerializeField] private GameObject startView;

    private int _triesCount = 0;

    private readonly List<LoginData> _loginData = new()
    {
        new LoginData("<b>Подсказка:</b> Дата выхода судебного модуля PJM", "15032016"),
        new LoginData("<b>Подсказка:</b> Тут что-то будет 1", "123"),
        new LoginData("<b>Подсказка:</b> Тут что-то будет 2", "1233")
    };

    private void Start()
    {
        startView.SetActive(false);
        loginView.SetActive(true);

        startButton.onClick.AddListener(Hide);
        loginButton.onClick.AddListener(OnLoginButtonClicked);

        hintText.text = _loginData[_triesCount].Hint;
    }

    private void OnLoginButtonClicked()
    {
        if (Login(inputField.text))
        {
            startView.SetActive(true);
            loginView.SetActive(false);
        }
        else
        {
            hintText.text = _loginData[_triesCount].Hint;

            (hintText.transform as RectTransform).DOShakeRotation(0.35f, new Vector3() { z = 2 }, 30, 90, false, ShakeRandomnessMode.Harmonic);
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        questions.gameObject.SetActive(true);
    }

    private bool Login(string password)
    {
        _triesCount++;

        if (password != _loginData[0].Password && _triesCount == 1)
        {
            return false;
        }

        if (password != _loginData[1].Password && _triesCount == 2)
        {
            return false;
        }

        return password != string.Empty && _triesCount > 2;
    }
}

public struct LoginData
{
    public string Hint { get; }
    public string Password { get; }

    public LoginData(string hint, string password)
    {
        Hint = hint;
        Password = password;
    }
}