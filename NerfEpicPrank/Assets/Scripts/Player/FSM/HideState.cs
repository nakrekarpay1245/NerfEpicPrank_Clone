using System;
using UnityEngine;

public class HideState : IState
{
    public Action<int> Callback;

    public HideState(Action<int> Callback)
    {
        this.Callback = Callback;
    }
    public void OnStateEnter()
    {
        Debug.Log("Hide Enter");
    }

    public void OnStateExit()
    {
        Debug.Log("Hide Exit");
    }

    public void OnStateFixedUpdate()
    {
        Debug.Log("Hide FixedUpdate");
    }

    public void OnStateUpdate()
    {
        Debug.Log("Hide Update");
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Callback(1);
        }
    }
}
