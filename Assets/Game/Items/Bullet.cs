using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    public float Speed = 20f;
    public int Damage = 40;
    Rigidbody2D _rigidbody;
    public int Value = 1;

    public override int Cost { get { return Value; } }

    void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.velocity = transform.right * Speed;
    }

    private void Update()
    {
        if(_rigidbody.velocity.magnitude < 1f)
        {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player" && collision.tag != "Upgrade")
        {
            var mouse = collision.GetComponent<Mouse>();
            if (mouse != null)
            {
                mouse.TakeDamage(Damage);
            }
            gameObject.SetActive(false);
        }
    }
}
