using System;
using UnityEngine;

public class PatrolState : IState
{
    public Action<int> Callback;

    public PatrolState(Action<int> Callback)
    {
        this.Callback = Callback;
    }
    public void OnStateEnter()
    {
        Debug.Log("Patrol Enter");
    }

    public void OnStateExit()
    {
        Debug.Log("Patrol Exit");
    }

    public void OnStateFixedUpdate()
    {
        Debug.Log("Patrol FixedUpdate");
    }

    public void OnStateUpdate()
    {
        Debug.Log("Patrol Update");
    }
}
