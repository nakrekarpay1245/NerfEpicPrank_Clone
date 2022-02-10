using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        gameObject.layer = 12;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        gameObject.GetComponentInChildren<TrailRenderer>().enabled = false;
    }
}
