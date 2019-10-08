using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Assets.Game.Items.Guns;

public class Weapon : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;
    DateTime _lastShot;
    TimeSpan _shotDelay;
    Player _player;
    Gun _gun;

    public GameObject Gun;
    public Transform FirePoint;
    public GameObject BulletPrefab;

    private void Start()
    {
        _spriteRenderer = Gun.GetComponent<SpriteRenderer>();
        _player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        _spriteRenderer.sprite = _gun.Sprite;
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

    public void EquipGun(Gun gun)
    {
        _gun = gun;
        _gun.SetFirePoint(FirePoint);
        _shotDelay = new TimeSpan(0, 0, 0, 0, Mathf.RoundToInt(1000f / _gun.FiringRate));
    }

    void Shoot()
    {
        var now = DateTime.Now;
        if (now - _shotDelay > _lastShot && _player.Ammo > 0)
        {
            _gun.Fire();
            _player.OnShotFired();
            _lastShot = now;
        }
    }
}
