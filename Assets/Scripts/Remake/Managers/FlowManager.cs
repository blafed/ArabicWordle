using System;
using UnityEngine;

public class FlowManager : Manager<FlowManager>
{

    
    protected  virtual void LateUpdate()
    {
        FlowIn.Clear();
    }
}
