using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Takip etme hızı")]
    private float followSpeed;

    [SerializeField]
    [Tooltip("Dönme hızı")]
    private float rotationSpeed;

    [SerializeField]
    [Tooltip("x ekseninde takip etme uzaklığı")]
    public float xOffset;

    [SerializeField]
    [Tooltip("z ekseninde takip etme uzaklığı")]
    public float zOffset;

    [SerializeField]
    [Tooltip("y ekseninde takip etme uzaklığı")]
    public float yOffset;

    [Tooltip("takip edilecek obje")]
    public GameObject target;

    public static CameraMovement instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void LateUpdate()
    {
        FollowTheTarget();
        LookToTarget();
    }
    private void LookToTarget()
    {
        Quaternion rotateTarget =
            Quaternion.LookRotation(PlayerController.instance.currentEnemy.transform.position -
            transform.position);

        Quaternion rotationTarget =
            new Quaternion(rotateTarget.x, rotateTarget.y, transform.rotation.z, rotateTarget.w);

        transform.rotation = Quaternion.RotateTowards(transform.rotation,
                    rotationTarget, Time.deltaTime * rotationSpeed);
        //Debug.Log("Look To Target + " + PlayerController.instance.currentEnemy.name + " / " + rotationTarget);
    }
    private void FollowTheTarget()
    {
        transform.position =
            Vector3.MoveTowards(transform.position, new Vector3(target.transform.position.x + xOffset,
               target.transform.position.y + yOffset, target.transform.position.z + zOffset),
               followSpeed * Time.deltaTime);
    }
}
