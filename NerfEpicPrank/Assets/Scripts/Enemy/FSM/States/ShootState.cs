using System;
using UnityEngine;

public class ShootState : IState
{
    public Action<int> Callback;

    public FieldOfView fieldOfView;
    public EnemyHealth enemyHealth;
    public EnemyAI enemyAI;
    public ShootState(Action<int> Callback, FieldOfView fieldOfView, EnemyHealth enemyHealth,
        EnemyAI enemyAI)
    {
        this.Callback = Callback;
        this.fieldOfView = fieldOfView;
        this.enemyHealth = enemyHealth;
        this.enemyAI = enemyAI;
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
