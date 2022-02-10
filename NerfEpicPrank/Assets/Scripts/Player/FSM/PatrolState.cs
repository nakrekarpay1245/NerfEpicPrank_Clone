using System;
using UnityEngine;

public class PatrolState : IState
{
    public Action<int> Callback;

    public float speed;

    public AudioSource audioSource;
    public Animator animator;

    public AudioClip audioClip;


    private Transform patrolPoint;

    public PatrolState(Action<int> Callback, float speed, AudioSource audioSource,
        Animator animator, AudioClip audioClip)
    {
        this.Callback = Callback;
        this.speed = speed;
        this.audioSource = audioSource;
        this.animator = animator;
        this.audioClip = audioClip;
    }
    public void OnStateEnter()
    {
        Debug.Log("Patrol Enter");

        animator.SetBool("isRun", true);
        animator.SetBool("isIdle", false);
        audioSource.clip = audioClip;
        patrolPoint = PlayerController.instance.patrolPoints[LevelManager.instance.patrolIndex];
    }

    public void OnStateExit()
    {
        Debug.Log("Patrol Exit");
    }

    public void OnStateFixedUpdate()
    {
        Debug.Log("Patrol FixedUpdate");
    }

    public void OnStateUpdate()
    {
        //Debug.Log("Patrol Point Name : " + patrolPoint.name +
        //    Vector3.Distance(PlayerController.instance.transform.position,
        //    patrolPoint.position));
        GoNextPatrolPoint();
        if (Vector3.Distance(PlayerController.instance.transform.position,
            patrolPoint.position) < 0.5f)
        {
            Callback(0);
        }
    }

    public void GoNextPatrolPoint()
    {
        LookToPatrolPoint();
        RunToPatrolPoint();
    }
    private void LookToPatrolPoint()
    {
        Quaternion rotationTarget = Quaternion.LookRotation(patrolPoint.position -
           PlayerController.instance.transform.position);
        PlayerController.instance.transform.rotation =
            Quaternion.RotateTowards(PlayerController.instance.transform.rotation,
            rotationTarget, Time.deltaTime * speed);
    }
    private void RunToPatrolPoint()
    {
        Vector3 followPosition = Vector3.MoveTowards(PlayerController.instance.transform.position,
           patrolPoint.position, Time.deltaTime * speed * 0.25f);
        PlayerController.instance.transform.position = followPosition;
    }
}
