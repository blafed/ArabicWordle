using TMPro;
using UnityEngine;
using UnityEngine.UI;

public interface IElement
{
    ElementCode code { get; }
    Transform transform { get; }
    GameObject gameObject { get; }

}

public interface IElementDetailed : IElement
{
    Button button { get; }
    Image image { get; }
    CanvasGroup canvasGroup { get; }
    TextMeshProUGUI text { get; }

}

public interface IPopup : IElement
{
    CanvasGroup canvasGroup { get; }
}


public enum ElementCode
    {
        None,
        Enter,
        Backspace,
        Letter,
        LetterLast = Letter + 31,
        Page_Intro,
        Page_Menu,
        Page_Game,
        Page_Store,
        PlayClassic,
        PlayDaily,
        Coins,
        HowToPlay,
        SettingsButton,
        StoreButton,
    }
