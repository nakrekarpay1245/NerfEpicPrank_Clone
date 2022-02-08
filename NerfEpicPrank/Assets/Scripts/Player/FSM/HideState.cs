using System;
using UnityEngine;

public class HideState : IState
{
    public Action<int> Callback;

    public Vector3 statePosition;
    public Quaternion stateRotation;

    public GameObject playerModel;
    
    public AudioSource audioSource;
    public Animator animator;

    public AudioClip audioClip;
    public HideState(Action<int> Callback, Vector3 statePosition, 
        Quaternion stateRotation, GameObject playerModel, AudioSource audioSource, 
        Animator animator, AudioClip audioClip)
    {
        this.Callback = Callback;
        this.statePosition = statePosition;
        this.stateRotation = stateRotation;
        this.playerModel = playerModel;
        this.audioSource = audioSource;
        this.animator = animator;
        this.audioClip = audioClip;
    }
    public void OnStateEnter()
    {
        Debug.Log("Hide Enter");
        playerModel.transform.localPosition = statePosition;
        playerModel.transform.localRotation = stateRotation;

        animator.SetBool("isRun", true);
        audioSource.clip = audioClip;
    }

    public void OnStateExit()
    {
        Debug.Log("Hide Exit");
    }

    public void OnStateFixedUpdate()
    {
        Debug.Log("Hide FixedUpdate");
    }

    public void OnStateUpdate()
    {
        Debug.Log("Hide Update");
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Callback(1);
        }
    }
}
