using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            PlayerController.instance.Run();
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("HideArea"))
        {
            PlayerController.instance.hidePosition = other.gameObject.GetComponent<HideArea>().hidePosition;
            PlayerController.instance.attackPosition = other.gameObject.GetComponent<HideArea>().attackPosition;
            PlayerController.instance.currentEnemy = other.gameObject.GetComponent<HideArea>().currentEnemy;
        }
    }
}
