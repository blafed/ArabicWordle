using System;
using UnityEngine;

public class Manager<T> : Singleton<T> where T: Singleton<T>
{
    private void OnEnable()
    {
        Init();
    }

    protected sealed override void OnAwake()
    {
    }

    protected virtual void Init()
    {
        isInit = true;
    }
    
    public virtual bool isInit { get; protected set; }
}

