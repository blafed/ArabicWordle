using System;
using UnityEngine;

/// <summary>
/// Contains functions to be called by UI elements (buttons, etc...)
/// </summary>
public class Callback : MonoBehaviour
{
    public static Callback instance;
    private void Awake()
    {
        instance = this;
    }

    public void ShowRewardedAd()
    {
        FlowIn.showRewardedAd = true;
    }

    public void PlayClassic()
    {
        FlowIn.playClassic = true;
    }
    public void Button(ElementCode element){}
}
