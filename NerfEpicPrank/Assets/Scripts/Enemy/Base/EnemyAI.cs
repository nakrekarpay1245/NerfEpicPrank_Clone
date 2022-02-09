﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    #region STATE PARAMETERS
    [Tooltip("Durum Makinesi")]
    [Header("Durum Makinesi ve Durumlar")]
    public StateMachine stateMachine;

    private ChaseState chaseState;
    private ShootState shootState;
    private IdleState idleState;
    private SuspicionState suspicionState;
    private SearchState searchState;
    #endregion

    #region COMPONENT PARAMETERS
    [Header("Components")]
    [HideInInspector] public FieldOfView fieldOfView;
    [HideInInspector] public Animator animator;
    [HideInInspector] public AudioSource audioSource;

    [Header("Düşman Obje")]
    [HideInInspector] public Transform target;
    #endregion

    #region OTHER PARAMETERS
    [Header("Hareket Hızı")]
    public float moveSpeed;

    [Header("Dönme Hızı")]
    public float rotateSpeed;

    [Header("Alarm Değişkenleri")]
    [Tooltip("Chase' e geçerken alarm süresi")]
    public float chaseAlarmTime;

    [Tooltip("Suspicion' a geçerken alarm süresi")]
    public float suspicionAlarmTime;

    [Tooltip("Search' a geçerken alarm süresi")]
    public float searchAlarmTime;

    public GameObject alarmDisplay;

    public AudioClip idleClip;
    public AudioClip runClip;
    public AudioClip searchClip;
    public AudioClip suspicionClip;
    #endregion


    public static EnemyAI instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        fieldOfView = GetComponent<FieldOfView>();
        target = fieldOfView.target.transform;
        StateGenerator();
        Idle();
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
    public void Chase()
    {
        stateMachine.ChangeStates(chaseState);
    }

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
    public void Search()
    {
        stateMachine.ChangeStates(searchState);
    }
    #endregion


    #region CALLBACKS
    private void ChaseCallback(int index)
    {
        if (index == 0)
        {
            Idle();
        }
        else if (index == 1)
        {
            Shoot();
        }
        else if (index == 2)
        {
            Suspicion();
        }
        else if (index == 4)
        {
            Search();
        }
        else
        {
            Chase();
        }
    }

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
        else if (index == 3)
        {
            Chase();
        }
        else if (index == 4)
        {
            Search();
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
        else if (index == 3)
        {
            Chase();
        }
        else if (index == 4)
        {
            Search();
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
        else if (index == 3)
        {
            Chase();
        }
        else if (index == 4)
        {
            Search();
        }
        else
        {
            Suspicion();
        }
    }
    private void SearchCallback(int index)
    {
        if (index == 0)
        {
            Idle();
        }
        else if (index == 1)
        {
            Shoot();
        }
        else if (index == 2)
        {
            Suspicion();
        }
        else if (index == 3)
        {
            Chase();
        }
        else
        {
            Search();
        }
    }

    #endregion


    #region NEW STATES  
    private void StateGenerator()
    {
        NewChase();
        NewShoot();
        NewIdle();
        NewSuspicion();
        NewSearch();
    }

    private ChaseState NewChase()
    {
        chaseState = new ChaseState(ChaseCallback, audioSource, animator, searchClip, moveSpeed);
        return chaseState;
    }
    private ShootState NewShoot()
    {
        shootState = new ShootState(ShootCallback);
        return shootState;
    }
    private IdleState NewIdle()
    {
        idleState = new IdleState(IdleCallback, audioSource, animator,
            idleClip, alarmDisplay);
        return idleState;
    }
    private SuspicionState NewSuspicion()
    {
        suspicionState = new SuspicionState(SuspicionCallback, audioSource, animator,
            searchClip, rotateSpeed, alarmDisplay);
        return suspicionState;
    }
    private SearchState NewSearch()
    {
        searchState = new SearchState(SearchCallback, audioSource, animator,
            searchClip, rotateSpeed, alarmDisplay);
        return searchState;
    }
    #endregion

}
