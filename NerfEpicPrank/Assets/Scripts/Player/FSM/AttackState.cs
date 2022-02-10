using System;
using UnityEngine;

public class AttackState : IState
{
    public Action<int> Callback;

    public Weapon weapon;

    public GameObject playerModel;

    public AudioSource audioSource;
    public Animator animator;

    public AudioClip audioClip;

    public float moveSpeed;

    public float rotationSpeed;
    public AttackState(Action<int> Callback, Weapon weapon, GameObject playerModel,
        AudioSource audioSource, Animator animator, AudioClip audioClip, float moveSpeed,
        float rotationSpeed)
    {
        this.Callback = Callback;
        this.weapon = weapon;
        this.playerModel = playerModel;
        this.audioSource = audioSource;
        this.animator = animator;
        this.audioClip = audioClip;
        this.moveSpeed = moveSpeed;
        this.rotationSpeed = rotationSpeed;
    }
    public void OnStateEnter()
    {
        //Debug.Log("Attack Enter");
        animator.SetTrigger("isAttack");
        audioSource.clip = audioClip;
    }

    public void OnStateExit()
    {
        //  Debug.Log("Attack Exit");
        playerModel.transform.localPosition = Vector3.zero;
    }

    public void OnStateFixedUpdate()
    {
        // Debug.Log("Attack FixedUpdate");
    }

    public void OnStateUpdate()
    {
        RunToPosition();
        LookToRotation();
        // Debug.Log("Attack Update");
        weapon.Fire();
        animator.SetTrigger("isAttack");
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Callback(0);
        }
    }
    private void RunToPosition()
    {
        Vector3 runPosition = Vector3.Lerp(PlayerController.instance.transform.position,
           PlayerController.instance.attackPosition, Time.deltaTime * moveSpeed * 2);
        PlayerController.instance.transform.position = runPosition;
    }

    public void LookToRotation()
    {
        // Debug.Log("Look To Enemy in Attack");
        Quaternion rotationTarget =
            Quaternion.LookRotation(PlayerController.instance.transform.position -
            PlayerController.instance.currentEnemy.transform.position);

        PlayerController.instance.transform.rotation =
            Quaternion.RotateTowards(PlayerController.instance.transform.rotation,
                rotationTarget, Time.deltaTime * rotationSpeed * 100);
    }
}
