using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private Button startButtonClicked;
    [Space]
    [SerializeField] private Questions questions;

    private int _triesCount;

    private readonly List<LoginData> _loginData = new()
    {
        new LoginData("Дата выхода судебного модуля PJM", "15032016"),
        new LoginData("Тут что-то будет", "123")
    };

    private void Start()
    {
        startButtonClicked.onClick.AddListener(Hide);
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