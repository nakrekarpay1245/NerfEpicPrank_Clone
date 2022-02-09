using System;
using UnityEngine;

public class IdleState : IState
{
    public Action<int> Callback;

    public AudioSource audioSource;
    public Animator animator;

    public AudioClip audioClip;

    public GameObject alarmDisplay;

    public float suspicionAlarmTimer;

    public FieldOfView fieldOfView;
    public EnemyHealth enemyHealth;
    public EnemyAI enemyAI;

    public IdleState(Action<int> Callback, AudioSource audioSource, Animator animator,
        AudioClip audioClip, GameObject alarmDisplay, FieldOfView fieldOfView, EnemyHealth enemyHealth,
        EnemyAI enemyAI)
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
        Debug.Log("Idle Enter");
        animator.SetBool("isIdle", true);
        audioSource.clip = audioClip;
        alarmDisplay.SetActive(false);
    }

    public void OnStateExit()
    {
        Debug.Log("Idle Exit");
    }

    public void OnStateFixedUpdate()
    {
        Debug.Log("Idle FixedUpdate");
    }

    public void OnStateUpdate()
    {
        suspicionAlarmTimer = Mathf.Clamp(suspicionAlarmTimer, 0, Mathf.Infinity);

        Debug.Log("Idle Update");

        if (fieldOfView.targetIsDetected)
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

        if (enemyHealth.impact)
        {
            Debug.Log("Idle to Search");
            Callback(4);
        }
    }

    private bool IdleToSuspicionAlarmControl()
    {
        //Debug.Log("Idle to Suspicion Control : " + suspicionAlarmTimer);

        suspicionAlarmTimer += Time.deltaTime;
        if (suspicionAlarmTimer >= enemyAI.suspicionAlarmTime)
        {
            Debug.Log("Idle to Suspicion");
            return true;
        }
        else
            return false;
    }
}
