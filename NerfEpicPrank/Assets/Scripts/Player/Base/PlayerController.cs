using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region STATE PARAMETERS
    [Tooltip("Durum Makinesi")]
    [Header("Durum Makinesi ve Durumlar")]
    public StateMachine stateMachine;

    private HideState hideState;
    private AttackState attackState;
    private RunState runState;
    private PatrolState patrolState;
    #endregion

    #region Wepaon PARAMETERS
    [Header("Silah")]
    public Weapon weapon;

    [Header("Sahte Silah Değişkenleri")]
    [Tooltip("Saniyede atılacak mermi sayısı")]
    public float fireRate;
    [HideInInspector] public float nextTimeToFire;
    [Tooltip("Her atılacak merminin vereceği hasar")]
    public float damage;
    #endregion

    [Header("Hareket Hızı")]
    public float speed;

    public static PlayerController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        StateGenerator();
        Hide();
    }

    private void Update()
    {
        stateMachine.UpdateStates();
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdateStates();
    }

    #region STATE CHANGERS
    public void Hide()
    {
        stateMachine.ChangeStates(hideState);
    }

    public void Attack()
    {
        stateMachine.ChangeStates(attackState);
    }

    public void Run()
    {
        stateMachine.ChangeStates(runState);
    }
    public void Patrol()
    {
        stateMachine.ChangeStates(patrolState);
    }
    #endregion


    #region CALLBACKS
    private void HideCallback(int index)
    {

        if (index == 1)
        {
            Attack();
        }
        else if (index == 2)
        {
            Run();
        }
        else if (index == 3)
        {
            Patrol();
        }
        else
        {
            Hide();
        }
    }
    private void AttackCallback(int index)
    {

        if (index == 0)
        {
            Hide();
        }
        else if (index == 2)
        {
            Run();
        }
        else if (index == 3)
        {
            Patrol();
        }
        else
        {
            Attack();
        }
    }
    private void RunCallback(int index)
    {
        if (index == 0)
        {
            Hide();
        }
        else if (index == 1)
        {
            Attack();
        }
        else if (index == 3)
        {
            Patrol();
        }
        else
        {
            Run();
        }
    }
    private void PatrolCallback(int index)
    {

        if (index == 0)
        {
            Hide();
        }
        else if (index == 1)
        {
            Attack();
        }
        else if (index == 2)
        {
            Run();
        }
        else
        {
            Patrol();
        }
    }
    #endregion

    #region NEW STATES  
    private void StateGenerator()
    {
        NewHide();
        NewAttack();
        NewRun();
        NewPatrol();
    }

    private HideState NewHide()
    {
        hideState = new HideState(HideCallback);
        return hideState;
    }

    private AttackState NewAttack()
    {
        attackState = new AttackState(AttackCallback, weapon);
        return attackState;
    }
    private RunState NewRun()
    {
        runState = new RunState(RunCallback);
        return runState;
    }
    private PatrolState NewPatrol()
    {
        patrolState = new PatrolState(PatrolCallback);
        return patrolState;
    }
    #endregion
}
