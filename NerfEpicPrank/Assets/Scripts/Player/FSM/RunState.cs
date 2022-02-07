using System;
using UnityEngine;

public class RunState : IState
{
    public Action<int> Callback;

    public RunState(Action<int> Callback)
    {
        this.Callback = Callback;
    }
    public void OnStateEnter()
    {
        Debug.Log("Run Enter");
    }

    public void OnStateExit()
    {
        Debug.Log("Run Exit");
    }

    public void OnStateFixedUpdate()
    {
        Debug.Log("Run FixedUpdate");
    }

    public void OnStateUpdate()
    {
        Debug.Log("Run Update");
    }
}
