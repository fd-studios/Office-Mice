﻿using Assets.Game.Items.Guns;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PlayerState { Standing, Walking, ShootingGun, Dead }

    Game _game;
    SpriteRenderer _spriteRenderer;
    Movement _movement;
    DateTime _lastHit;
    TimeSpan _hitDelay;
    Weapon _weapon;

    public Sprite Standing;
    public Sprite Walking;
    public Sprite ShootingGun;
    public Sprite ShootingMachineGun;
    public Sprite Dead;

    AudioSource _audioSource;
    public AudioClip Reload;
    public AudioClip Damage;
    public AudioClip PowerUp;

    public PlayerState State = PlayerState.Standing;
    public int BaseHealth = 200;
    public float Health { get; private set; }
    public int BaseAmmo = 50;
    public int Ammo { get; private set; }
    public int RespawnDelay = 5;
    public GameObject StartingGun;
    public GameObject Body;

    public ToastHandler ToastPanel;


    // Start is called before the first frame update
    void Start()
    {
        _game = GameObject.FindObjectOfType<Game>();
        _audioSource = GetComponent<AudioSource>();
        _spriteRenderer = Body.GetComponent<SpriteRenderer>();
        _movement = GetComponent<Movement>();
        _weapon = GetComponent<Weapon>();
        _weapon.EquipGun(StartingGun.GetComponent<Gun>());
        _hitDelay = new TimeSpan(0, 0, 0, 0, 500);
        Health = BaseHealth;
        Ammo = BaseAmmo;
    }

    PlayerState _lastState = PlayerState.Dead;

    // Update is called once per frame
    void Update()
    {
        if (State == _lastState) return;
        _lastState = State;
        _spriteRenderer.sortingOrder = 1;

        switch (State)
        {
            case PlayerState.Standing:
                _spriteRenderer.sprite = Standing;
                break;
            case PlayerState.Walking:
                _spriteRenderer.sprite = Walking;
                break;
            case PlayerState.ShootingGun:
                if(_weapon.HasGun)
                    _spriteRenderer.sprite = ShootingGun;
                break;
            case PlayerState.Dead:
                _spriteRenderer.sprite = Dead;
                _spriteRenderer.sortingOrder = 0;
                break;
        }
    }

    public void UpgradeWeapon(Gun gun, float duration)
    {
        _weapon.EquipGun(gun);

        if(duration > 0f)
            StartCoroutine(Downgrade(duration));

        _audioSource.clip = PowerUp;
        _audioSource.Play();

        ToastPanel.ToastWeaponUpgrade(gun.ToastImage);
    }

    IEnumerator Downgrade(float duration)
    {
        yield return new WaitForSeconds(duration);

        _weapon.EquipGun(StartingGun.GetComponent<Gun>());
        //OnDowngrade();
        yield break;
    }

    void OnDowngrade()
    {
        ToastPanel.ToastWeaponDowngrade();
    }

    public void TakeDamage(float damage)
    {
        if (Health <= 0) return;

        var now = DateTime.Now;
        if (now - _hitDelay > _lastHit)
        {
            Health -= damage;
            _lastHit = now;
            _weapon.EquipGun(StartingGun.GetComponent<Gun>());
            if (Health <= 0)
            {
                Die();
            }
            if (Damage != null && Health <= 50)
            {
                _audioSource.clip = Damage;
                _audioSource.Play();
            }
            _movement.Run(1.5f, 2);
        }
    }

    public void Die()
    {
        State = PlayerState.Dead;
        transform.Find("Bleed").gameObject.SetActive(true);

        _game.Respwan(gameObject, RespawnDelay, OnRespawn);
    }

    public void OnRespawn()
    {
        _hitDelay = new TimeSpan(0, 0, 0, 0, 500);
        Health = BaseHealth;
        Ammo = BaseAmmo;
        transform.position = new Vector3(0, 1, -1);
        transform.rotation = new Quaternion();
        State = PlayerState.Standing;
        transform.Find("Bleed").gameObject.SetActive(false);

        var go = FindObjectOfType<WaveSpawner>().Begin;
        go.Play();

        OnDowngrade();
    }

    public void AddAmmo(int ammo)
    {
        if (Ammo <= 0 && !_audioSource.isPlaying)
        {
            _audioSource.clip = Reload;
            _audioSource.Play();
        }

        Ammo += ammo;

        ToastPanel.ToastAmmo();
    }

    public void OnShotFired(int projectileValue)
    {
        Ammo -= projectileValue;
    }

    public void AddHealth(int health)
    {
        Health += health;
        if (Health > BaseHealth) Health = BaseHealth;
    }

    public void AddSpeed(float timeOut)
    {
        _movement.Run(timeOut, 3);
    }
}
