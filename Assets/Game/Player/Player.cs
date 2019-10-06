﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PlayerState { Standing, Walking, ShootingGun }

    Game _game;
    SpriteRenderer _spriteRenderer;
    Movement _movement;
    bool upgradedWeapon = false;
    DateTime _lastHit;
    TimeSpan _hitDelay;

    public Sprite Standing;
    public Sprite Walking;
    public Sprite ShootingGun;
    public Sprite ShootingMachineGun;

    public PlayerState State = PlayerState.Standing;
    public bool UpgradedWeapon { get { return upgradedWeapon; } }
    public float FiringRate = 4;
    public int BaseHealth = 200;
    public float Health { get; private set; }
    public int BaseAmmo = 50;
    public int Ammo { get; private set; }
    public int RespawnDelay = 5;

    public ToastHandler ToastPanel;


    // Start is called before the first frame update
    void Start()
    {
        _game = GameObject.FindObjectOfType<Game>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _movement = GetComponent<Movement>();
        _hitDelay = new TimeSpan(0, 0, 0, 0, 500);
        Health = BaseHealth;
        Ammo = BaseAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        switch (State)
        {
            case PlayerState.Standing:
                _spriteRenderer.sprite = Standing;
                break;
            case PlayerState.Walking:
                _spriteRenderer.sprite = Walking;
                break;
            case PlayerState.ShootingGun:
                if (UpgradedWeapon)
                {
                    _spriteRenderer.sprite = ShootingMachineGun;
                    FiringRate = 20;
                }
                else
                {
                    _spriteRenderer.sprite = ShootingGun;
                    FiringRate = 8;
                }
                break;
        }
    }

    public async void UpgradeWeapon(float duration)
    {
        Debug.Log($"Weapone Upgraded");
        upgradedWeapon = true;
        StartCoroutine(Downgrade(duration));

        await ToastPanel.ToastWeaponUpgrade();
    }

    IEnumerator Downgrade(float duration)
    {
        yield return new WaitForSeconds(duration);
        Debug.Log($"Weapone Downgraded");
        upgradedWeapon = false;
        yield break;
    }

    public void TakeDamage(float damage)
    {
        var now = DateTime.Now;
        if (now - _hitDelay > _lastHit)
        {
            Health -= damage;
            _lastHit = now;
            Debug.Log($"Player hit: {damage} Health: {Health}");
            if (Health <= 0)
            {
                Die();
            }
            _movement.Run();
        }
    }

    public void Die()
    {
        Debug.Log($"Player died");
        gameObject.SetActive(false);
        _game.Respwan(gameObject, RespawnDelay, OnRespawn);
    }

    public void OnRespawn()
    {
        Debug.Log($"Player respawned");
        _hitDelay = new TimeSpan(0, 0, 0, 0, 500);
        Health = BaseHealth;
        Ammo = BaseAmmo;
        transform.position = new Vector3(0, 1, -1);
        transform.rotation = new Quaternion();

        var go = FindObjectOfType<WaveSpawner>().Begin;
        go.Play();
    }

    public async void AddAmmo(int ammo)
    {
        Ammo += ammo;

        await ToastPanel.ToastAmmo();
    }

    public void OnShotFired()
    {
        Ammo -= 1;
    }

    public void AddHealth(int health)
    {
        Health += health;
        if (Health > BaseHealth) Health = BaseHealth;
    }
}
