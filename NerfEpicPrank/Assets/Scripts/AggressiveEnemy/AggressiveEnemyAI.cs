using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveEnemyAI : MonoBehaviour
{
    #region STATE PARAMETERS
    [Tooltip("Durum Makinesi")]
    [Header("Durum Makinesi ve Durumlar")]
    public StateMachine stateMachine;

    private aShootState shootState;
    private aIdleState idleState;
    private aSuspicionState suspicionState;
    #endregion

    #region COMPONENT PARAMETERS
    [Header("Components")]
    [HideInInspector] public FieldOfView fieldOfView;
    [HideInInspector] public EnemyHealth enemyHealth;
    [HideInInspector] public Animator animator;
    [HideInInspector] public AudioSource audioSource;

    [Header("Düşman Obje")]
    [HideInInspector] public Transform target;
    #endregion

    #region OTHER PARAMETERS
    [Header("Dönme Hızı")]
    public float rotateSpeed;

    [Header("Alarm Değişkenleri")]
    [Tooltip("Chase' e geçerken alarm süresi")]
    public float shootAlarmTime;

    [Tooltip("Suspicion' a geçerken alarm süresi")]
    public float suspicionAlarmTime;

    public GameObject alarmDisplay;

    public AudioClip idleClip;
    public AudioClip suspicionClip;

    public Weapon weapon;
    #endregion
    public GameObject localCanvas;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponentInChildren<Animator>();
        fieldOfView = GetComponent<FieldOfView>();
        enemyHealth = GetComponent<EnemyHealth>();
        stateMachine = GetComponent<StateMachine>();
        target = fieldOfView.target.transform;
        StateGenerator();
        Idle();
    }
    private void Update()
    {
        stateMachine.UpdateStates();
        localCanvas.transform.rotation = Camera.main.transform.rotation;
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdateStates();
    }

    #region STATE CHANGERS
    public void Shoot()
    {
        stateMachine.ChangeStates(shootState);
    }

    public void Idle()
    {
        stateMachine.ChangeStates(idleState);
    }
    public void Suspicion()
    {
        stateMachine.ChangeStates(suspicionState);
    }
    #endregion


    #region CALLBACKS
    private void ShootCallback(int index)
    {
        if (index == 0)
        {
            Idle();
        }
        else if (index == 2)
        {
            Suspicion();
        }
        else
        {
            Shoot();
        }
    }

    private void IdleCallback(int index)
    {
        if (index == 1)
        {
            Shoot();
        }
        else if (index == 2)
        {
            Suspicion();
        }
        else
        {
            Idle();
        }
    }

    private void SuspicionCallback(int index)
    {
        if (index == 0)
        {
            Idle();
        }
        else if (index == 1)
        {
            Shoot();
        }
        else
        {
            Suspicion();
        }
    }

    #endregion


    #region NEW STATES  
    private void StateGenerator()
    {
        NewShoot();
        NewIdle();
        NewSuspicion();
    }


    private aShootState NewShoot()
    {
        shootState = new aShootState(ShootCallback, audioSource, animator,
            fieldOfView, enemyHealth, weapon, rotateSpeed, alarmDisplay, this);
        return shootState;
    }
    private aIdleState NewIdle()
    {
        idleState = new aIdleState(IdleCallback, audioSource, animator, idleClip, alarmDisplay,
            fieldOfView, enemyHealth, this);
        return idleState;
    }
    private aSuspicionState NewSuspicion()
    {
        suspicionState = new aSuspicionState(SuspicionCallback, audioSource, animator,
            suspicionClip, rotateSpeed, alarmDisplay, fieldOfView, enemyHealth, this);
        return suspicionState;
    }
    #endregion
}
