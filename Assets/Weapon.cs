using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject Player;
    public Transform FirePoint;
    public GameObject BulletPrefab;

    // Update is called once per frame
    void Update()
    {
        var movement = Player.GetComponent<Movement>();
        Debug.Log(movement);
        if (movement != null && movement.Firing)
        {
            Shoot();
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
    }
}
