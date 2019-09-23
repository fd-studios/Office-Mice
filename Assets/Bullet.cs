using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 20f;
    public int Damage = 40;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void OnEnable()
    {
        rb.velocity = transform.right * Speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name != "Player")
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
