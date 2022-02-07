using System;
using UnityEngine;

public class ChaseState : IState
{
    public Action<int> Callback;

    public ChaseState(Action<int> Callback)
    {
        this.Callback = Callback;
    }
    public void OnStateEnter()
    {
        Debug.Log("Game Over");
        PlayerController.instance.Run();
    }

    public void OnStateExit()
    {
        Debug.Log("Chase Exit");
    }

    public void OnStateFixedUpdate()
    {
        Debug.Log("Chase FixedUpdate");
    }

    public void OnStateUpdate()
    {
        Debug.Log("Chase Update");
    }
}
