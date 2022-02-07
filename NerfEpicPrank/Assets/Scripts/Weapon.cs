﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float fireRate;
    [SerializeField] private float nextTimeToFire;

    [SerializeField] private float bulletForce;

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform muzzle;

    private void Awake()
    {
        nextTimeToFire = fireRate;
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

        Destroy(currentBullet, 20);
    }
}
