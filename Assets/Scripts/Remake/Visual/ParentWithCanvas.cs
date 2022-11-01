using System;
using UnityEngine;

public class ParentWithCanvas : MonoBehaviour
{
    private static Canvas canvas;
    private void Awake()
    {
        if(!canvas)
        canvas =  FindObjectOfType<Canvas>();
        transform.SetParent(canvas.transform, false);
    }
}
