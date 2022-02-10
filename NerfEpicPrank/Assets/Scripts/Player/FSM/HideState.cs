using System;
using UnityEngine;

public class HideState : IState
{
    public Action<int> Callback;

    public GameObject playerModel;

    public AudioSource audioSource;
    public Animator animator;

    public AudioClip audioClip;

    public float speed;
    public HideState(Action<int> Callback, GameObject playerModel, AudioSource audioSource,
        Animator animator, AudioClip audioClip, float speed)
    {
        this.Callback = Callback;
        this.playerModel = playerModel;
        this.audioSource = audioSource;
        this.animator = animator;
        this.audioClip = audioClip;
        this.speed = speed;
    }
    public void OnStateEnter()
    {
        //Debug.Log("Hide Enter");
        animator.SetBool("isIdle", true);
        animator.SetBool("isRun", false);
        audioSource.clip = audioClip;
    }

    public void OnStateExit()
    {
        //Debug.Log("Hide Exit");
    }

    public void OnStateFixedUpdate()
    {
       // Debug.Log("Hide FixedUpdate");
    }

    public void OnStateUpdate()
    {
        RunToPosition();

       // Debug.Log("Hide Update");
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Callback(1);
        }
    }
    private void RunToPosition()
    {
        Vector3 runPosition = Vector3.Lerp(PlayerController.instance.transform.position,
           PlayerController.instance.hidePosition, Time.deltaTime * speed * 2);
        PlayerController.instance.transform.position = runPosition;
    }
}
