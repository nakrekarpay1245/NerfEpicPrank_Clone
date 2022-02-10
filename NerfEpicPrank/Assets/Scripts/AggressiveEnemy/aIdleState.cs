using System;
using System.Collections.Generic;
using UnityEngine;

public class aIdleState : IState
{
    public Action<int> Callback;

    public AudioSource audioSource;
    public Animator animator;

    public AudioClip audioClip;

    public GameObject alarmDisplay;

    public float suspicionAlarmTimer;

    public FieldOfView fieldOfView;
    public EnemyHealth enemyHealth;
    public AggressiveEnemyAI enemyAI;

    public aIdleState(Action<int> Callback, AudioSource audioSource, Animator animator,
        AudioClip audioClip, GameObject alarmDisplay, FieldOfView fieldOfView, EnemyHealth enemyHealth,
        AggressiveEnemyAI enemyAI)
    {
        this.Callback = Callback;
        this.audioSource = audioSource;
        this.animator = animator;
        this.audioClip = audioClip;
        this.alarmDisplay = alarmDisplay;
        this.fieldOfView = fieldOfView;
        this.enemyHealth = enemyHealth;
        this.enemyAI = enemyAI;
    }
    public void OnStateEnter()
    {
        //Debug.Log("Idle Enter");
        animator.SetBool("isIdle", true);
        animator.SetBool("isSuspicion", false);
        audioSource.clip = audioClip;
        alarmDisplay.SetActive(false);
        fieldOfView.viewMeshFilter.gameObject.SetActive(false);
    }

    public void OnStateExit()
    {
        // Debug.Log("Idle Exit");
    }

    public void OnStateFixedUpdate()
    {
        //  Debug.Log("Idle FixedUpdate");
    }

    public void OnStateUpdate()
    {
        suspicionAlarmTimer = Mathf.Clamp(suspicionAlarmTimer, 0, Mathf.Infinity);

        //  Debug.Log("Idle Update");

        if (fieldOfView.targetIsDetected || enemyHealth.impact)
        {
            if (IdleToSuspicionAlarmControl())
            {
                Callback(2);
            }
        }
        else
        {
            suspicionAlarmTimer -= Time.deltaTime;

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }

    private bool IdleToSuspicionAlarmControl()
    {
        //Debug.Log("Idle to Suspicion Control : " + suspicionAlarmTimer);

        suspicionAlarmTimer += Time.deltaTime;
        if (suspicionAlarmTimer >= enemyAI.suspicionAlarmTime)
        {
            //   Debug.Log("Idle to Suspicion");
            return true;
        }
        else
            return false;
    }
}
