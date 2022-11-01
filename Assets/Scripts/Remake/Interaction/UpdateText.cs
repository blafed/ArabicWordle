using System;
using UnityEngine;

public class UpdateText : MonoBehaviour
{
    public FlowOutText flowText;

    private IElementDetailed elementDetailed;


    private void Start()
    {
        elementDetailed = GetComponent<IElementDetailed>();
    }

    private void Update()
    {
        elementDetailed.text.text = FlowOut.GetText(flowText);
    }
}
