using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    public EnemyHealth enemyHealth;
    private void Awake()
    {
       enemyHealth =  gameObject.GetComponent<EnemyHealth>();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            enemyHealth.TakeDamage();
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;
            other.gameObject.layer = 12;
        }
    }
}
