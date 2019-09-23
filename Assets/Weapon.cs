using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject BulletPrefab;
    DateTime _lastShot;
    TimeSpan _shotDelay;
    Player _player;

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        _shotDelay = new TimeSpan(0, 0, 0, 0, Mathf.RoundToInt(1000f / _player.FiringRate));
        var movement = GetComponent<Movement>();
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
        if (now - _shotDelay > _lastShot)
        {
            GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject("Bullet");
            if (bullet != null)
            {
                bullet.transform.position = FirePoint.position;
                bullet.transform.rotation = FirePoint.rotation;
                bullet.SetActive(true);
            }
            _lastShot = now;
        }
    }
}
