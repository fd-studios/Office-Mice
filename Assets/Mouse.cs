using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    public int Health = 100;

    public GameObject deathEffect;

    public void TakeDamage(int damage)
    {
        Health -= damage;
        Debug.Log($"Mouse Hit Health:{Health}");

        if (Health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Mouse dead");
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
