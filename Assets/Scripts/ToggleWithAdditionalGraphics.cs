using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToggleWithAdditionalGraphics : Toggle
{
    [SerializeField] private AdditionlGraphics[] additionalGraphics;
    [Space]
    [SerializeField] private TextMeshProUGUI text;

    public void SetText(string value) => text.text = value;

    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        base.DoStateTransition(state, instant);

        if (additionalGraphics == null || additionalGraphics.Length == 0)
            return;

        foreach (var graphics in additionalGraphics) 
        {
            if (graphics.Graphic != null)
                graphics.Graphic.CrossFadeColor(GetColor(state, graphics.ColorBlock), graphics.ColorBlock.fadeDuration, false, true);
        }

        Color GetColor (SelectionState state, ColorBlock colorBlock)
        {
            return state switch
            {
                SelectionState.Normal => colorBlock.normalColor,
                SelectionState.Highlighted => colorBlock.highlightedColor,
                SelectionState.Pressed => colorBlock.pressedColor,
                SelectionState.Selected => colorBlock.selectedColor,
                SelectionState.Disabled => colorBlock.disabledColor,
                _ => Color.white,
            };
        }
    }

    [System.Serializable]
    public struct AdditionlGraphics
    {
        public Graphic Graphic;
        public ColorBlock ColorBlock;
    }
}