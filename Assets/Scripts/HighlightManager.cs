using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class HighlightManager : Singleton<HighlightManager>
{
    public float duration = 1;
    public float alphaChange = 0.7f;
    List<ItemInfo> highlightables = new List<ItemInfo>();
    
    public class ItemInfo
    {
        public IHighlightable highlightable;
        public Color defaultColor;
        public bool defaultInteractable;

        public Button Button => highlightable.Button;
        public Image Image => highlightable.Image;
        public UIElement Element => highlightable.Element;
    }


    private List<ItemInfo> effectedItems = new List<ItemInfo>();

    public void Add(IHighlightable item)
    {
        
        highlightables.Add(new ItemInfo
        {
            highlightable = item,
        });
    }

    public void HighlightKeyboard()
    {
        var keyboard = new UIElement[34];
        keyboard[33] = UIElement.enter;
        keyboard[32] = UIElement.backspace;
        for (int i = 0; i < 32; i++)
        {
            keyboard[i] = UIElement.letter + i;
        }
        Highlight(keyboard);
    }
    public void Highlight(params UIElement[] pElements)
    {
        Highlight(elements: pElements);
    }
    public void Highlight(IEnumerable<UIElement> elements)
    {
        foreach (var x in highlightables)
        {

            bool existInElements = false;
            foreach (var y in elements)
            {
                if (y == x.Element)
                {
                    existInElements = true;
                    break;
                }
            }

            if (effectedItems.Contains(x))
                continue;
            if (x.Image)
                x.defaultColor = x.Image.color;
            if (x.Button)
            {
                x.defaultInteractable = x.Button.interactable;
                x.Button.interactable = true;
            }
            if (existInElements)
                StartCoroutine(Highlight(x));
            else
            {
                x.Button.interactable = false;
                var color = x.Image.color;
                color.a *= alphaChange;
                x.Image.color = color;
            }
        }
    }

    public void UnHihghlight()
    {
        StopAllCoroutines();
        foreach (var h in effectedItems)
        {
            if (h.Button)
                h.Button.interactable = h.defaultInteractable;
            if (h.Image)
                h.Image.color = h.defaultColor;
        }
    }

    IEnumerator Highlight(ItemInfo h)
    {
        effectedItems.Add(h);


        if (!h.Image)
            yield break;
        var targetColor = h.defaultColor;
        targetColor.a *= this.alphaChange;
        float t = 0;
        while (true)
        {
            t += Time.deltaTime;
            var p = t / duration;
            if (p <= 0.5f)
                h.Image.color = Color.Lerp(h.defaultColor, targetColor, p / 0.5f);
            else
                h.Image.color = Color.Lerp(h.defaultColor, targetColor, 1 - p / 0.5f);

            if (p >= 1)
                t = 0;

            yield return null;
        }
    }
}

public enum UIElement
{
    none,
    hint,
    eliminate,
    coins,
    back,
    enter,
    backspace,
    letter,
    letterLast = letter + 31,
    RESET,
}
