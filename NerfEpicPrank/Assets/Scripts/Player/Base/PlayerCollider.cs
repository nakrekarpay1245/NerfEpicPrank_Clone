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
            //Debug.Log("Player Hitted");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("HideArea"))
        {
            PlayerController.instance.hidePosition = other.gameObject.GetComponent<HideArea>().hidePosition;
            PlayerController.instance.attackPosition = other.gameObject.GetComponent<HideArea>().attackPosition;
            PlayerController.instance.currentEnemy = other.gameObject.GetComponent<HideArea>().currentEnemy;
            CameraMovement.instance.xOffset = other.gameObject.GetComponent<HideArea>().xOffset;
            CameraMovement.instance.yOffset = other.gameObject.GetComponent<HideArea>().yOffset;
            CameraMovement.instance.zOffset = other.gameObject.GetComponent<HideArea>().zOffset;
        }
    }
}
