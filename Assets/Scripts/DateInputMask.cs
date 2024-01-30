using UnityEngine;
using System.Text.RegularExpressions;

public class DateInputMask : MonoBehaviour
{
    public TMPro.TMP_InputField inputField;

    private void Awake()
    {
        inputField.onValueChanged.AddListener(delegate { OnValueChanged(); });
    }

    public void OnValueChanged()
    {
        if (string.IsNullOrEmpty(inputField.text))
        {
            inputField.text = string.Empty;
        }
        else
        {
            var input = inputField.text;
            var matchPattern = @"^((\d{2}/){0,2}(\d{1,2})?)$";
            var replacementPattern = "$1/$3";
            var toReplacePattern = @"((\.?\d{2})+)(\d)";

            input = Regex.Replace(input, toReplacePattern, replacementPattern);
            var result = Regex.Match(input, matchPattern);

            if (!result.Success)
            {
                return;
            }
            
            inputField.text = input;
            inputField.stringPosition = input.Length;
        }
    }
}