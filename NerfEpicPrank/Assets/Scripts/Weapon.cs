using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    [Header("Silah saniyede atış sayısı")]
    private float fireRate;
    
    private float nextTimeToFire;

    [SerializeField]
    [Header("Silah mermi fırlatma hızı")]
    private float bulletForce;

    [SerializeField]
    [Header("Silah mermi sapma miktarı")]
    private float deviation;

    [SerializeField]
    [Header("Silah mermi preafbi")]
    private GameObject bullet;

    [SerializeField]
    [Header("Silah mermi çıkma noktası(namlu)")]
    private Transform muzzle;


    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        nextTimeToFire = 1;
        audioSource = GetComponent<AudioSource>();
    }
    public void Fire()
    {
        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1 / fireRate;
            FireWeapon();
        }
    }

    private void FireWeapon()
    {
        GameObject currentBullet = Instantiate(bullet, muzzle.position,
            Quaternion.EulerAngles(-90, 0, 0));

        currentBullet.GetComponent<Rigidbody>().AddForce(bulletForce *
            (transform.forward + (new Vector3(2,0.25f,0) * Random.Range(-deviation,deviation))) * Time.deltaTime * 100);

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }

        Destroy(currentBullet, 7);
    }
}
