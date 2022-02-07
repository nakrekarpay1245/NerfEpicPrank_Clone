using System;
using UnityEngine;

public class SearchState : IState
{
    public Action<int> Callback;

    public SearchState(Action<int> Callback)
    {
        this.Callback = Callback;
    }
    public void OnStateEnter()
    {
        Debug.Log("Search Enter");
    }

    public void OnStateExit()
    {
        Debug.Log("Search Exit");
    }

    public void OnStateFixedUpdate()
    {
        Debug.Log("Search FixedUpdate");
    }

    public void OnStateUpdate()
    {
        if (FieldOfView.instance.targetIsDetected)
        {
            Callback(2);
        }
    }
}
