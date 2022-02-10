using System;
using System.Collections.Generic;
using UnityEngine;

public class aSuspicionState : IState
{
    public Action<int> Callback;

    public AudioSource audioSource;
    public Animator animator;

    public AudioClip audioClip;

    public float speed;

    public GameObject alarmDisplay;

    public float shootAlarmTimer;

    public FieldOfView fieldOfView;
    public EnemyHealth enemyHealth;

    public AggressiveEnemyAI enemyAI;
    public aSuspicionState(Action<int> Callback, AudioSource audioSource, Animator animator,
        AudioClip audioClip, float speed, GameObject alarmDisplay, FieldOfView fieldOfView,
        EnemyHealth enemyHealth, AggressiveEnemyAI enemyAI)
    {
        this.Callback = Callback;
        this.audioSource = audioSource;
        this.animator = animator;
        this.audioClip = audioClip;
        this.speed = speed;
        this.alarmDisplay = alarmDisplay;
        this.fieldOfView = fieldOfView;
        this.enemyHealth = enemyHealth;
        this.enemyAI = enemyAI;
    }

    public void OnStateEnter()
    {
        Debug.Log("Suspicion Enter");
        animator.SetBool("isSuspicion", true);
        animator.SetBool("isIdle", false);
        audioSource.clip = audioClip;
        alarmDisplay.SetActive(true);
        enemyHealth.impact = false;
    }

    public void OnStateExit()
    {
        Debug.Log("Suspicion Exit");
    }

    public void OnStateFixedUpdate()
    {
        Debug.Log("Suspicion FixedUpdate");
    }

    public void OnStateUpdate()
    {
        shootAlarmTimer = Mathf.Clamp(shootAlarmTimer, 0, Mathf.Infinity);
        Debug.Log("Suspicion Update");
        if (fieldOfView.targetIsDetected)
        {
            Debug.Log("Target is detected");
            if (SuspicionToShootAlarmControl())
            {
                Debug.Log("to shoot");
                Callback(1);
            }
            else
            {
                Debug.Log("look target");
                LookToTarget();
            }
        }
        else
        {
            Debug.Log("Target is not detected");
            shootAlarmTimer -= Time.deltaTime;
            LookToTarget();
        }
    }

    private void LookToTarget()
    {
        Debug.Log("Look To Target");
        Quaternion rotationTarget = Quaternion.LookRotation(fieldOfView.targetPosition -
            enemyAI.transform.position);
        enemyAI.transform.rotation = Quaternion.RotateTowards(enemyAI.transform.rotation,
            rotationTarget, Time.deltaTime * speed);
    }

    private bool SuspicionToShootAlarmControl()
    {
        //Debug.Log("Suspicion to Shoot Control : " + shootAlarmTimer);

        shootAlarmTimer += Time.deltaTime;
        if (shootAlarmTimer >= enemyAI.shootAlarmTime)
        {
            Debug.Log("Suspicion to Shoot");
            return true;
        }
        else
            return false;
    }
}
