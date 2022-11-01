using System.Collections.Generic;
using UnityEngine;
using System.Collections;
public class ElementManager : Manager<ElementManager>
{

    public IReadOnlyList<IElement> elements => _elements;
    List<IElement> _elements = new List<IElement>();

    private ElementCode openedCode;
    private IElement opened;
    
    protected override void Init()
    {
        _elements = new List<IElement>(Resources.FindObjectsOfTypeAll<Element>());
    }

    public IElement Find(ElementCode code)
    {
        return _elements.Find(x => x.code == code);
    }

    public void OpenPopup(ElementCode code)
    {
        foreach (var x in _elements)
            if (x is IPopup popup)
                StartCoroutine(FadePopup(popup, x.code == code));
    }

    IEnumerator FadePopup(IPopup popup, bool fadeIn)
    {
        
        if(popup.gameObject.activeSelf == fadeIn)
            yield break;
        popup.gameObject.SetActive(fadeIn);
        float alphaFrom = fadeIn ? 0 : 1;
        float alphaTo = fadeIn ? 1 : 0;

        float t = 0;
        while (t < UIConfig.instance.popupFadeTime)
        {
            t += Time.fixedDeltaTime;
            var alpha = Mathf.Lerp(alphaFrom, alphaTo, t / UIConfig.instance.popupFadeTime);
            popup.canvasGroup.alpha = alpha;
            yield return null;
        }
    }
}

public abstract class Element : MonoBehaviour, IElement
{
    public abstract ElementCode code { get; }
}
