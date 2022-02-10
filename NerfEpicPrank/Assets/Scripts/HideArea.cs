using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideArea : MonoBehaviour
{
    [Tooltip("Gizlenme pozisyonu")]
    public Vector3 hidePosition;

    [Tooltip("Saldırı pozisyonu")]
    public Vector3 attackPosition;

    [Tooltip("O an salıdırılacak düşman")]
    public GameObject currentEnemy;

    [Tooltip("Kamera x ekseni uzaklığı")]
    public float xOffset;

    [Tooltip("Kamera z ekseni uzaklığı")]
    public float zOffset;

    [Tooltip("Kamera y ekseni uzaklığı")]
    public float yOffset;
}
