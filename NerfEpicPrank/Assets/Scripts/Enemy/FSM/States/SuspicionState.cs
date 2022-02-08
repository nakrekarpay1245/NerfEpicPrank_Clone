using System;
using UnityEngine;

public class SuspicionState : IState
{
    public Action<int> Callback;

    public AudioSource audioSource;
    public Animator animator;

    public AudioClip audioClip;

    public float speed;

    public GameObject alarmDisplay;

    public float chaseAlarmTimer;
    public float searchAlarmTimer;
    public SuspicionState(Action<int> Callback, AudioSource audioSource, Animator animator,
        AudioClip audioClip, float speed, GameObject alarmDisplay)
    {
        this.Callback = Callback;
        this.audioSource = audioSource;
        this.animator = animator;
        this.audioClip = audioClip;
        this.speed = speed;
        this.alarmDisplay = alarmDisplay;
    }

    public void OnStateEnter()
    {
        Debug.Log("Suspicion Enter");
        animator.SetBool("isIdle", true);
        audioSource.clip = audioClip;
        alarmDisplay.SetActive(true);
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
        chaseAlarmTimer = Mathf.Clamp(chaseAlarmTimer, 0, Mathf.Infinity);
        Debug.Log("Suspicion Update");
        if (!FieldOfView.instance.targetIsDetected)
        {
            chaseAlarmTimer -= Time.deltaTime;

            //Debug.Log("Target is not detected");

            if (FieldOfView.instance.targetInFieldOfView)
            {
                //Debug.Log("Target in fov");

                //FieldOfView.instance.lastSeen = false;
                //sus to sear
                if (SuspicionToSearchAlarmControl())
                {
                    //Debug.Log("not search");
                    Callback(4);
                }
                else
                {
                    //Debug.Log("Else");
                    LookToTarget();
                }
            }
            else
            {
                //Debug.Log("Target not in fov");
                LookToTarget();
            }
        }
        else if (FieldOfView.instance.targetIsDetected)
        {
            searchAlarmTimer = 0;

            //Debug.Log("Target detected");
            if (SuspicionToChaseAlarmControl())
            {
                Callback(3);
            }
            else
            {
                LookToTarget();
            }
        }
        else
        {
            chaseAlarmTimer -= Time.deltaTime;
            LookToTarget();
        }
    }

    private void LookToTarget()
    {
        //Debug.Log("Look To Target");
        Quaternion rotationTarget = Quaternion.LookRotation(FieldOfView.instance.targetPosition -
            EnemyAI.instance.transform.position);
        EnemyAI.instance.transform.rotation = Quaternion.RotateTowards(EnemyAI.instance.transform.rotation,
            rotationTarget, Time.deltaTime * speed);
    }


    private bool SuspicionToChaseAlarmControl()
    {
        Debug.Log("Suspicion to Chase Control : " + chaseAlarmTimer);

        chaseAlarmTimer += Time.deltaTime;
        if (chaseAlarmTimer >= EnemyAI.instance.chaseAlarmTime)
        {
            Debug.Log("Suspicion to Chase");
            return true;
        }
        else
            return false;
    }

    private bool SuspicionToSearchAlarmControl()
    {
        Debug.Log("Suspicion to Search Control");

        searchAlarmTimer += Time.deltaTime;
        if (searchAlarmTimer >= EnemyAI.instance.searchAlarmTime)
        {
            Debug.Log("Suspicion to Search");
            return true;
        }
        else
            return false;
    }
}
