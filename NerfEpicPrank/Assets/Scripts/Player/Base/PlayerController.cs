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

    public Vector3 hidePosition;

    public Vector3 attackPosition;

    public Transform exitTransform;
    #endregion

    #region COMPONENT PARAMETERS
    [Header("Components")]
    [HideInInspector] public Animator animator;
    [HideInInspector] public AudioSource audioSource;
    #endregion

    #region Wepaon PARAMETERS
    [Header("Silah")]
    public Weapon weapon;
    #endregion

    #region Other PARAMETERS
    [Header("Hareket Hızı")]
    public float moveSpeed;

    [Header("Hareket Hızı")]
    public float rotationSpeed;

    public GameObject playerModel;

    public AudioClip attackClip;
    public AudioClip hideClip;
    public AudioClip runClip;
    #endregion

    public List<Transform> patrolPoints;

    public static PlayerController instance;

    public GameObject currentEnemy;

   // public Quaternion startRotation;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        audioSource = playerModel.GetComponent<AudioSource>();
        animator = playerModel.GetComponent<Animator>();
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
        hideState = new HideState(HideCallback, playerModel, audioSource,
            animator, hideClip, moveSpeed);
        return hideState;
    }

    private AttackState NewAttack()
    {
        attackState = new AttackState(AttackCallback, weapon, playerModel,
            audioSource, animator, attackClip, moveSpeed, rotationSpeed);
        return attackState;
    }
    private RunState NewRun()
    {
        runState = new RunState(RunCallback, moveSpeed, rotationSpeed, exitTransform, audioSource, animator,
            attackClip);
        return runState;
    }
    private PatrolState NewPatrol()
    {
        patrolState = new PatrolState(PatrolCallback, moveSpeed, audioSource, animator, attackClip);
        return patrolState;
    }
    #endregion
}
