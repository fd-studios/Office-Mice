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
    public AudioSource FiringSound;

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
        if (now - _shotDelay > _lastShot && _player.Ammo > 0)
        {
            GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject("Bullet");
            if (bullet != null)
            {
                bullet.transform.position = FirePoint.position;
                bullet.transform.rotation = FirePoint.rotation;
                bullet.SetActive(true);
            }
            if (FiringSound != null) FiringSound.Play();
            _player.OnShotFired();
            _lastShot = now;
        }
    }
}
