using System;
using UnityEngine;

public class AttackState : IState
{
    public Action<int> Callback;

    public Weapon weapon;
    public AttackState(Action<int> Callback, Weapon weapon)
    {
        this.Callback = Callback;
        this.weapon = weapon;
    }
    public void OnStateEnter()
    {
        Debug.Log("Attack Enter");
    }

    public void OnStateExit()
    {
        Debug.Log("Attack Exit");
    }

    public void OnStateFixedUpdate()
    {
        Debug.Log("Attack FixedUpdate");
    }

    public void OnStateUpdate()
    {
        Debug.Log("Attack Update");
        weapon.Fire();
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Callback(0);
        }
    }
}
