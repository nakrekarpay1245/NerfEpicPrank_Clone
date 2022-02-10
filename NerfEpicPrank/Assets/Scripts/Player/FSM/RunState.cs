using System;
using UnityEngine;

public class RunState : IState
{
    public Action<int> Callback;

    public float moveSpeed;
    public float rotationSpeed;
    public Transform exitTransform;

    public AudioSource audioSource;
    public Animator animator;

    public AudioClip audioClip;
    public RunState(Action<int> Callback, float moveSpeed, float rotationSpeed, Transform exitTransform,
        AudioSource audioSource, Animator animator, AudioClip audioClip)
    {
        this.Callback = Callback;
        this.moveSpeed = moveSpeed;
        this.rotationSpeed = rotationSpeed;
        this.exitTransform = exitTransform;
        this.audioSource = audioSource;
        this.animator = animator;
        this.audioClip = audioClip;
    }
    public void OnStateEnter()
    {
        Debug.Log("Run Enter");
        Debug.Log("Level Failed On Player Run State");
        LevelManager.instance.LevelFailed();

        animator.SetBool("isRun", true);
        animator.SetBool("isIdle", false);
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
        Debug.Log("Look To Exit");
        Quaternion rotationTarget = Quaternion.LookRotation(PlayerController.instance.transform.position -
            exitTransform.position);
        PlayerController.instance.transform.rotation =
            Quaternion.RotateTowards(PlayerController.instance.transform.rotation,
            rotationTarget, Time.deltaTime * rotationSpeed * 100);
    }
    private void RunToExit()
    {
        Vector3 followPosition = Vector3.Lerp(PlayerController.instance.transform.position,
           exitTransform.position, Time.deltaTime * moveSpeed * 0.25f);
        PlayerController.instance.transform.position = followPosition;
    }
}
