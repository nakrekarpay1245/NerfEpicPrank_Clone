using System;
using UnityEngine;

public class ShootState : IState
{
    public Action<int> Callback;

    public ShootState(Action<int> Callback)
    {
        this.Callback = Callback;
    }
    public void OnStateEnter()
    {
        Debug.Log("Shoot Enter");
    }

    public void OnStateExit()
    {
        Debug.Log("Shoot Exit");
    }

    public void OnStateFixedUpdate()
    {
        Debug.Log("Shoot FixedUpdate");
    }

    public void OnStateUpdate()
    {
        Debug.Log("Shoot Update");
    }
}
