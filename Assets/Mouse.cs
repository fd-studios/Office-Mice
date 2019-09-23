using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour, IEnemy
{
    Vector2 _movement;
    float someScale;

    public int Health = 120;

    public GameObject hitEffect;
    public GameObject deathEffect;
    public Rigidbody2D rb;
    public float speed = 5f;

    public string Name { get; set; }
    public Transform Transform { get; set; }


    // Use this for initialization
    void Start()
    {
        Transform = GetComponentInChildren<Transform>();
        rb = GetComponent<Rigidbody2D>();
        someScale = transform.localScale.x;
    }

    void Update()
    {
        _movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        Transform.eulerAngles = _movement;
        if (rb.velocity.x >= 0)
        {
            transform.localScale = new Vector2(-someScale, transform.localScale.y);
        }
        else
        {
            transform.localScale = new Vector2(someScale, transform.localScale.y);
        }
    }

    void FixedUpdate()
    {
        rb.AddForce(_movement * speed);
    }

    public void TakeDamage(int damage)
    {
        var position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
        var effect = Instantiate(hitEffect, position, Quaternion.identity);
        effect.transform.localScale = transform.localScale;
        Health -= damage;
        Debug.Log($"Mouse Hit Health:{Health}");

        if (Health <= 0)
        {
            Die();
        }
        Destroy(effect, 0.08f);
    }

    void Die()
    {
        Debug.Log("Mouse dead");
        var deadMouse = Instantiate(deathEffect, transform.position, Quaternion.identity);
        deadMouse.transform.localScale = transform.localScale;
        Destroy(gameObject);
        var player = GameObject.FindGameObjectWithTag("Player");
        Score score = player.GetComponentInChildren<Score>();
        score.IncreaseScore(1);
        Destroy(deadMouse, 2f);
    }
}
