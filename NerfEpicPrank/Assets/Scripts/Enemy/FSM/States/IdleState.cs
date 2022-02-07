using System;
using UnityEngine;

public class IdleState : IState
{
    public Action<int> Callback;

    public IdleState(Action<int> Callback)
    {
        this.Callback = Callback;
    }
    public void OnStateEnter()
    {
        Debug.Log("Idle Enter");
    }

    public void OnStateExit()
    {
        Debug.Log("Idle Exit");
    }

    public void OnStateFixedUpdate()
    {
        Debug.Log("Idle FixedUpdate");
    }

    public void OnStateUpdate()
    {
        if (FieldOfView.instance.targetIsDetected)
        {
            Callback(2);
        }
    }
}
