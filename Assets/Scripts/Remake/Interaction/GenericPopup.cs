using System;
using UnityEngine;

namespace Update
{
    public class GenericPopup : GenericElement, IPopup
    {
        public CanvasGroup canvasGroup { get; private set; }

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
    }
}