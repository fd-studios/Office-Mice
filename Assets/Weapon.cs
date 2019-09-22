using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject Player;
    public Transform FirePoint;
    public GameObject BulletPrefab;
    private bool allowFiring = true;

    // Update is called once per frame
    void Update()
    {
        var movement = Player.GetComponent<Movement>();
        Debug.Log(movement);
        if (movement != null && movement.Firing)
        {
            StartCoroutine(Shoot());
        }
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        if (allowFiring)
        {
            allowFiring = false;
            Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
            yield return new WaitForSeconds(0.2f);
            allowFiring = true;
        }
    }
}
