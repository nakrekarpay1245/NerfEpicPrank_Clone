using System;
using UnityEngine;

public class RunState : IState
{
    public Action<int> Callback;

    public float speed;
    public Transform exitTransform;

    public AudioSource audioSource;
    public Animator animator;

    public AudioClip audioClip;
    public RunState(Action<int> Callback, float speed, Transform exitTransform, AudioSource audioSource,
        Animator animator, AudioClip audioClip)
    {
        this.Callback = Callback;
        this.speed = speed;
        this.exitTransform = exitTransform;
        this.audioSource = audioSource;
        this.animator = animator;
        this.audioClip = audioClip;
    }
    public void OnStateEnter()
    {
        Debug.Log("Run Enter");

        animator.SetBool("isRun", true);
        audioSource.clip = audioClip;
    }

    public void OnStateExit()
    {
        Debug.Log("Run Exit");
    }

    public void OnStateFixedUpdate()
    {
        Debug.Log("Run FixedUpdate");
    }

    public void OnStateUpdate()
    {
        Debug.Log("Run Update");
        LookToExit();
        RunToExit();
    }
    private void LookToExit()
    {
        Quaternion rotationTarget = Quaternion.LookRotation(exitTransform.position -
           PlayerController.instance.transform.position);
        PlayerController.instance.transform.rotation =
            Quaternion.RotateTowards(PlayerController.instance.transform.rotation,
            rotationTarget, Time.deltaTime * speed);
    }
    private void RunToExit()
    {
        Vector3 followPosition = Vector3.Lerp(PlayerController.instance.transform.position,
           exitTransform.position, Time.deltaTime * speed * 0.25f);
        PlayerController.instance.transform.position = followPosition;
    }
}
