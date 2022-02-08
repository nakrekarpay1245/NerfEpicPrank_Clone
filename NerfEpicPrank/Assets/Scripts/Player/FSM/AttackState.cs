using System;
using UnityEngine;

public class AttackState : IState
{
    public Action<int> Callback;

    public Weapon weapon;

    public Vector3 statePosition;
    public Quaternion stateRotation;

    public GameObject playerModel;

    public AudioSource audioSource;
    public Animator animator;

    public AudioClip audioClip;

    public AttackState(Action<int> Callback, Weapon weapon, Vector3 statePosition,
        Quaternion stateRotation, GameObject playerModel, AudioSource audioSource, 
        Animator animator, AudioClip audioClip)
    {
        this.Callback = Callback;
        this.weapon = weapon;
        this.statePosition = statePosition;
        this.stateRotation = stateRotation;
        this.playerModel = playerModel;
        this.audioSource = audioSource;
        this.animator = animator;
        this.audioClip = audioClip;
    }
    public void OnStateEnter()
    {
        Debug.Log("Attack Enter");
        playerModel.transform.localPosition = statePosition;
        playerModel.transform.localRotation = stateRotation;

        animator.SetBool("isRun", true);
        audioSource.clip = audioClip;
    }

    public void OnStateExit()
    {
        Debug.Log("Attack Exit");
        playerModel.transform.localPosition = Vector3.zero;
    }

    public void OnStateFixedUpdate()
    {
        Debug.Log("Attack FixedUpdate");
    }

    public void OnStateUpdate()
    {
        Debug.Log("Attack Update");
        weapon.Fire();
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Callback(0);
        }
    }
}
