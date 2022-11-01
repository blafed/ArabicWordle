using System;
using RTLTMPro;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonElement : Element, IElementDetailed
{
    [SerializeField] ElementCode _code;
    public override ElementCode code => _code;
    
    public Button button { get; private set; }
    public TextMeshProUGUI text { get; private set; } 
    public Image image => button.image;

    public CanvasGroup canvasGroup  { get; private set; }
    
    private void Awake()
    {
        button = GetComponent<Button>();
        text = GetComponentInChildren<RTLTextMeshPro>();
        canvasGroup = GetComponent<CanvasGroup>();
        button.onClick.AddListener(Click);
    }

    public void Click()
    {
        Callback.instance.Button(code);
    }
}
