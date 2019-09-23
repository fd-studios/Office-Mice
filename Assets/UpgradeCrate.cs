using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCrate : MonoBehaviour
{
    public float Duration = 10f;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();
            player.UpgradeWeapon(Duration);
            player.RespwanCrate(gameObject);
            gameObject.SetActive(false);
        }
    }
}
