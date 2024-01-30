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
    [Space]
    [SerializeField] private Image background;
    [SerializeField] private RectTransform label;
    [SerializeField] private RectTransform title;

    private int _triesCount = 0;
    private bool _isAuthorized = false; 
    private bool _animationIsPlayed = false; 
    private bool _shouldWorkOnEnterClick = true; 

    private readonly List<LoginData> _loginData = new()
    {
        new LoginData("<b>Подсказка:</b> Дата выхода судебного модуля PJM", "15032016"),
        new LoginData("<b>Подсказка:</b> Сумма всех цифр равна 18", "15032016"),
        new LoginData("<b>Подсказка:</b> Это был запоздалый подарок девушкам на 8 марта", "15032016")
    };

    private void Start()
    {
        startView.SetActive(false);
        loginView.SetActive(true);

        startButton.onClick.AddListener(Hide);
        loginButton.onClick.AddListener(OnLoginButtonClicked);

        hintText.text = _loginData[_triesCount].Hint;
    }

    private void Update()
    {
        if (!_shouldWorkOnEnterClick)
        {
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.Return) && !_isAuthorized)
        {
            OnLoginButtonClicked();
        }
        
        if (Input.GetKeyDown(KeyCode.Return) && _animationIsPlayed)
        {
            Hide();
        }
    }

    private void OnLoginButtonClicked()
    {
        if (Login(inputField.text))
        {
            _isAuthorized = true;
            PlayShowAnimation();
        }
        else
        {
            hintText.text = _loginData[_triesCount].Hint;

            (hintText.transform as RectTransform).DOShakeRotation(0.35f, new Vector3() { z = 2 }, 30, 90, false, ShakeRandomnessMode.Harmonic);
        }
    }

    private void PlayShowAnimation()
    {
        startView.gameObject.SetActive(true);

        background.DOFade(1, 0.25f).From(0);

        title.DOMove(title.transform.position, 0.25f).From(title.transform.position + Vector3.down * 1100f).SetEase(Ease.OutBack).SetDelay(0.05f);
        label.DOMove(label.transform.position, 0.25f).From(label.transform.position + Vector3.down * 800f).SetEase(Ease.OutBack).SetDelay(0.075f).OnComplete(() =>
        {
            _animationIsPlayed = true;
        });
    }

    public void Hide()
    {
        _shouldWorkOnEnterClick = false;
            
        loginView.gameObject.SetActive(false);

        background.DOFade(0, 0.35f).SetDelay(0.2f);

        title.DOMove(title.transform.position + Vector3.up * 500f, 0.35f);
        label.DOMove(label.transform.position - Vector3.up * 1100f, 0.35f).OnComplete(() => gameObject.SetActive(false));

        questions.gameObject.SetActive(true);
    }

    private bool Login(string password)
    {
        _triesCount++;

        password = password.Replace("/", "");

        if (password != _loginData[0].Password && _triesCount == 1)
        {
            return false;
        }

        if (password != _loginData[1].Password && _triesCount == 2)
        {
            return false;
        }

        return password != string.Empty && _triesCount > 2 || password == _loginData[0].Password;
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