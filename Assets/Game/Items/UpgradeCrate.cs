using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCrate : PowerUp
{
    public int Ammo = 100;
    public float Duration = 10f;

    public override void OnPickup(Player player)
    {
        player.AddAmmo(Ammo);
        player.UpgradeWeapon(Duration);
    }
}
