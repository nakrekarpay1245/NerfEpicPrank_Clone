using System;
using UnityEngine;

public class IdleState : IState
{
    public Action<int> Callback;

    public AudioSource audioSource;
    public Animator animator;

    public AudioClip audioClip;

    public GameObject alarmDisplay;

    public IdleState(Action<int> Callback, AudioSource audioSource, Animator animator,
        AudioClip audioClip, GameObject alarmDisplay)
    {
        this.Callback = Callback;
        this.audioSource = audioSource;
        this.animator = animator;
        this.audioClip = audioClip;
        this.alarmDisplay = alarmDisplay;
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
        if (FieldOfView.instance.targetIsDetected)
        {
            Callback(2);
        }
        else
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}
