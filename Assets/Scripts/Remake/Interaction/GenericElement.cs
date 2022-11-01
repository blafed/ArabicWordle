using System;
using RTLTMPro;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Update
{
    public class GenericElement : Element, IElementDetailed
    {
        [SerializeField] ElementCode _code;

        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI _text;
        
        public override ElementCode code => _code;
        public Button button => _button;
        public Image image => _image;
        public CanvasGroup canvasGroup => _canvasGroup;
        public TextMeshProUGUI text => _text;

        protected virtual void Reset()
        {
            _button = GetComponent<Button>();
            _image = GetComponent<Image>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _text = GetComponent<RTLTextMeshPro>();
        }
    }
}