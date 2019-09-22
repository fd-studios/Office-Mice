using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject Player;
    public Transform FirePoint;
    public GameObject BulletPrefab;
    private DateTime lastShot;
    private TimeSpan shotDelay = new TimeSpan(0, 0, 0, 0, 250);


    // Update is called once per frame
    void Update()
    {
        var movement = Player.GetComponent<Movement>();
        if (movement != null && movement.Firing)
        {
            Shoot();
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        var now = DateTime.Now;
        if (now - shotDelay > lastShot)
        {
            Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
            lastShot = now;
        }
    }
}
