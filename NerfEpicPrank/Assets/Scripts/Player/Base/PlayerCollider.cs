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
            other.gameObject.GetComponentInChildren<TrailRenderer>().enabled = false;
            other.gameObject.layer = 12;
            Debug.Log("Player Hitted");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("HideArea"))
        {
            PlayerController.instance.hidePosition = other.gameObject.GetComponent<HideArea>().hidePosition;
            PlayerController.instance.attackPosition = other.gameObject.GetComponent<HideArea>().attackPosition;
            PlayerController.instance.currentEnemy = other.gameObject.GetComponent<HideArea>().currentEnemy;
            //other.gameObject.GetComponent<HideArea>().currentEnemy.SetActive(true);
        }
    }
}
