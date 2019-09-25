using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 20f;
    public int Damage = 40;
    Rigidbody2D _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
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
