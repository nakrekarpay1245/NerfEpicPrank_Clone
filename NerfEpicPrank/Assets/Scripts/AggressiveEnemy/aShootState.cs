using System;
using System.Collections.Generic;
using UnityEngine;

public class aShootState : IState
{
    public Action<int> Callback;

    public AudioSource audioSource;
    public Animator animator;

    public FieldOfView fieldOfView;
    public EnemyHealth enemyHealth;
    public GameObject alarmDisplay;

    public AggressiveEnemyAI enemyAI;

    public Weapon weapon;
    public float speed;

    float nextTimeToFire;
    float fireRate = 5;
    bool isShootTime;
    public aShootState(Action<int> Callback, AudioSource audioSource, Animator animator,
        FieldOfView fieldOfView, EnemyHealth enemyHealth, Weapon weapon, float speed,
        GameObject alarmDisplay, AggressiveEnemyAI enemyAI)
    {
        this.Callback = Callback;
        this.audioSource = audioSource;
        this.animator = animator;
        this.fieldOfView = fieldOfView;
        this.enemyHealth = enemyHealth;
        this.weapon = weapon;
        this.speed = speed;
        this.alarmDisplay = alarmDisplay;
        this.enemyAI = enemyAI;
    }
    public void OnStateEnter()
    {
        //  Debug.Log("Shoot Enter");
        isShootTime = true;
        fieldOfView.viewMeshFilter.gameObject.SetActive(true);
    }

    public void OnStateExit()
    {
        //  Debug.Log("Shoot Exit");
    }

    public void OnStateFixedUpdate()
    {
        // Debug.Log("Shoot FixedUpdate");
    }

    public void OnStateUpdate()
    {
        // Debug.Log("Shoot Update");

        ShootTime();

        LookToTarget();


        if (isShootTime)
        {
            //Debug.Log("Shoot Time");
            animator.SetTrigger("isShoot");
            alarmDisplay.SetActive(true);
            weapon.Fire();
        }
        else
        {
            // Debug.Log("No Shoot Time");
            alarmDisplay.SetActive(false);
        }
    }
    private void LookToTarget()
    {
        // Debug.Log("Look To Target");
        Quaternion rotationTarget = Quaternion.LookRotation(fieldOfView.targetPosition -
            enemyAI.transform.position);
        enemyAI.transform.rotation = Quaternion.RotateTowards(enemyAI.transform.rotation,
            rotationTarget, Time.deltaTime * speed);
    }

    private void ShootTime()
    {
        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + fireRate;
            isShootTime = !isShootTime;
        }
    }
}
