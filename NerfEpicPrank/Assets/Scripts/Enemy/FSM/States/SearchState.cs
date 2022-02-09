using System;
using UnityEngine;

public class SearchState : IState
{
    public Action<int> Callback;

    public AudioSource audioSource;
    public Animator animator;

    public AudioClip audioClip;

    public float speed;

    public GameObject alarmDisplay;

    public float suspicionAlarmTimer;

    public FieldOfView fieldOfView;
    public EnemyHealth enemyHealth;
    public EnemyAI enemyAI;

    public SearchState(Action<int> Callback, AudioSource audioSource, Animator animator,
        AudioClip audioClip, float speed, GameObject alarmDisplay, FieldOfView fieldOfView, 
        EnemyHealth enemyHealth, EnemyAI enemyAI)
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
        Debug.Log("Search Enter");
        animator.SetBool("isIdle", true);
        audioSource.clip = audioClip;
        alarmDisplay.SetActive(false);
    }

    public void OnStateExit()
    {
        Debug.Log("Search Exit");
    }

    public void OnStateFixedUpdate()
    {
        Debug.Log("Search FixedUpdate");
    }

    public void OnStateUpdate()
    {
        suspicionAlarmTimer = Mathf.Clamp(suspicionAlarmTimer, 0, Mathf.Infinity);

        Debug.Log("Search Update");

        if (fieldOfView.targetIsDetected || enemyHealth.impact)
        {
            if (SearchToSuspicionAlarmControl())
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
            RotateAround();
        }
    }

    private void RotateAround()
    {
        enemyAI.transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }
    private bool SearchToSuspicionAlarmControl()
    {
        //Debug.Log("Search to Chase suspicion : " + suspicionAlarmTimer);

        suspicionAlarmTimer += Time.deltaTime;
        if (suspicionAlarmTimer >= enemyAI.suspicionAlarmTime)
        {
            Debug.Log("Search to Suspicion");
            return true;
        }
        else
            return false;
    }
}
