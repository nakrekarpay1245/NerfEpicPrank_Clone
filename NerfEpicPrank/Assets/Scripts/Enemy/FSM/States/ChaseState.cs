using System;
using UnityEngine;

public class ChaseState : IState
{
    public Action<int> Callback;

    public AudioSource audioSource;
    public Animator animator;

    public AudioClip audioClip;

    public float speed;
    public ChaseState(Action<int> Callback, AudioSource audioSource, Animator animator,
        AudioClip audioClip, float speed)
    {
        this.Callback = Callback;
        this.audioSource = audioSource;
        this.animator = animator;
        this.audioClip = audioClip;
        this.speed = speed;
    }
    public void OnStateEnter()
    {
        Debug.Log("Game Over");

        animator.SetBool("isRun", true);
        audioSource.clip = audioClip;

        PlayerController.instance.Run();
    }

    public void OnStateExit()
    {
        Debug.Log("Chase Exit");
    }

    public void OnStateFixedUpdate()
    {
        Debug.Log("Chase FixedUpdate");
    }

    public void OnStateUpdate()
    {
        Debug.Log("Chase Update");
        //look and follow the target
        LookToTarget();
        FollowTarget();
    }

    private void LookToTarget()
    {
        Debug.Log("Look To Target in Chase");
        Quaternion rotationTarget = Quaternion.LookRotation(FieldOfView.instance.targetPosition -
            EnemyAI.instance.transform.position);
        EnemyAI.instance.transform.rotation = Quaternion.RotateTowards(EnemyAI.instance.transform.rotation,
            rotationTarget, Time.deltaTime * speed);
    }
    private void FollowTarget()
    {
        Debug.Log("Follow To Target in Chase");
        Vector3 followPosition = Vector3.Lerp(EnemyAI.instance.transform.position,
            FieldOfView.instance.targetPosition, Time.deltaTime * speed * 0.25f);
        EnemyAI.instance.transform.position = followPosition;
    }
}
