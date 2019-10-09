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
        var movement = GetComponent<Movement>();
        if (movement != null && movement.Firing)
        {
            _spriteRenderer.sprite = _gun.Sprite;
            Shoot();
        }
        else if (Input.GetButtonDown("Fire1"))
        {
            _spriteRenderer.sprite = _gun.Sprite;
            Shoot();
        }
        else
        {
            _spriteRenderer.sprite = null;
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
        var minAmmo = _gun?.Projectile?.Cost ?? 1;
        if (now - _shotDelay > _lastShot && _player.Ammo >= minAmmo)
        {
            _gun.Fire();
            _player.OnShotFired(minAmmo);
            _lastShot = now;
        }
    }
}
