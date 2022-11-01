using System;
using RTLTMPro;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

    public class KeyboardKey : Element, IElementDetailed
    {
        public string value;

        private Button _button;
        private TextMeshProUGUI _text;
        

        public override ElementCode code => ElementCode.Letter + WordArray.LetterCode(value);
        public Button button => _button;
        public Image image => button.image;
        public CanvasGroup canvasGroup { get; }
        public TextMeshProUGUI text => _text;

        private void Awake()
        {
            _text = GetComponentInChildren<RTLTextMeshPro>();
            _button = GetComponent<Button>();
        }
    }

