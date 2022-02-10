using System;
using UnityEngine;
using UnityEngine.UI;

public class SuspicionState : IState
{
    public Action<int> Callback;

    public AudioSource audioSource;
    public Animator animator;

    public AudioClip audioClip;

    public float speed;

    public GameObject alarmDisplay;
    public Image alarmImage;

    public float chaseAlarmTimer;
    public float searchAlarmTimer;

    public FieldOfView fieldOfView;
    public EnemyHealth enemyHealth;
    public EnemyAI enemyAI;
    public SuspicionState(Action<int> Callback, AudioSource audioSource, Animator animator,
        AudioClip audioClip, float speed, GameObject alarmDisplay, Image alarmImage,
        FieldOfView fieldOfView, EnemyHealth enemyHealth, EnemyAI enemyAI)
    {
        this.Callback = Callback;
        this.audioSource = audioSource;
        this.animator = animator;
        this.audioClip = audioClip;
        this.speed = speed;
        this.alarmDisplay = alarmDisplay;
        this.alarmImage = alarmImage;
        this.fieldOfView = fieldOfView;
        this.enemyHealth = enemyHealth;
        this.enemyAI = enemyAI;
    }

    public void OnStateEnter()
    {
        //Debug.Log("Suspicion Enter");
        animator.SetBool("isSuspicion", true);
        animator.SetBool("isIdle", false);
        animator.SetBool("isRun", false);
        audioSource.clip = audioClip;
        alarmDisplay.SetActive(true);
        enemyHealth.impact = false;

        fieldOfView.viewMeshFilter.gameObject.SetActive(true);
    }

    public void OnStateExit()
    {
        //Debug.Log("Suspicion Exit");
    }

    public void OnStateFixedUpdate()
    {
        //Debug.Log("Suspicion FixedUpdate");
    }

    public void OnStateUpdate()
    {
        // Debug.Log("Suspicion Update");

        chaseAlarmTimer = Mathf.Clamp(chaseAlarmTimer, 0, Mathf.Infinity);
        alarmImage.fillAmount = chaseAlarmTimer / enemyAI.chaseAlarmTime;
        if (!fieldOfView.targetIsDetected)
        {
            chaseAlarmTimer -= Time.deltaTime;

            //Debug.Log("Target is not detected");

            if (fieldOfView.targetInFieldOfView)
            {
                //Debug.Log("Target in fov");

                //sus to sear
                if (SuspicionToSearchAlarmControl())
                {
                    Callback(4);
                }
                else
                {
                    LookToTarget();
                }
            }
            else
            {
                //Debug.Log("Target not in fov");
                LookToTarget();
            }
        }
        else if (fieldOfView.targetIsDetected)
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
        Quaternion rotationTarget = Quaternion.LookRotation(fieldOfView.targetPosition -
            enemyAI.transform.position);
        enemyAI.transform.rotation = Quaternion.RotateTowards(enemyAI.transform.rotation,
            rotationTarget, Time.deltaTime * speed);
    }

    private bool SuspicionToChaseAlarmControl()
    {
        //Debug.Log("Suspicion to Chase Control : " + chaseAlarmTimer);

        chaseAlarmTimer += Time.deltaTime;
        if (chaseAlarmTimer >= enemyAI.chaseAlarmTime)
        {
            //  Debug.Log("Suspicion to Chase");
            return true;
        }
        else
            return false;
    }

    private bool SuspicionToSearchAlarmControl()
    {
        //Debug.Log("Suspicion to Search Control");

        searchAlarmTimer += Time.deltaTime;
        if (searchAlarmTimer >= enemyAI.searchAlarmTime)
        {
            //Debug.Log("Suspicion to Search");
            return true;
        }
        else
            return false;
    }
}
