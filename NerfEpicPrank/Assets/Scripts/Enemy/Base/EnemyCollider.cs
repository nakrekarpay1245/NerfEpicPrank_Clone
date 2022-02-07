using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            Debug.Log("nabre");
            EnemyAI.instance.Suspicion();
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
