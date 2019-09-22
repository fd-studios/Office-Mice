using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    public int Health = 100;

    public GameObject hitEffect;
    public GameObject deathEffect;

    public void TakeDamage(int damage)
    {
        var effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Health -= damage;
        Debug.Log($"Mouse Hit Health:{Health}");

        if (Health <= 0)
        {
            Die();
        }
        Destroy(effect);
    }

    void Die()
    {
        Debug.Log("Mouse dead");
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
