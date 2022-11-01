using System;
using UnityEngine;
using UnityEngine.UI;
public class KeyboardButton : MonoBehaviour,IHighlightable
{
    public string letter;

    public int Code
    {
        get
        {
            if (!code.HasValue)
            {
                int value = WordArray.LetterCode(letter);
                code = value;
                return value;
            }
            return code.Value;
        }
    }
    int? code;


    public UIElement Element => UIElement.letter +  Code;
    public Button Button => button;
    public Color DefaultColor => defaultColor;
    public Image Image => Button.image;


    Button button;
    Button.ButtonClickedEvent currentEvent;

    private Color defaultColor;


    void Start()
    {
        button = GetComponent<Button>();

        currentEvent = button.onClick;
        button.onClick = new Button.ButtonClickedEvent();
        button.onClick.AddListener(HandleClick);
        PagesManager.Instance.highlightables.Add(this);
    }
    void HandleClick() {
        currentEvent.Invoke();
    }
    
}