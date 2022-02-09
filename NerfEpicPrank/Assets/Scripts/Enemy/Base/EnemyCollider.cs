using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            EnemyHealth.instance.TakeDamage();
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;
            other.gameObject.layer = 12;
        }
    }
}
