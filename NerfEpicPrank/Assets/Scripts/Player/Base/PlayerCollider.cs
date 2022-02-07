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
}
