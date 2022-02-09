using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float fireRate;
    [SerializeField] private float nextTimeToFire;

    [SerializeField] private float bulletForce;

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform muzzle;


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
            transform.forward * Time.deltaTime * 100);

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }

        Destroy(currentBullet, 7);
    }
}
