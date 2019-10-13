using Assets.Game.Items.Guns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCrate : PowerUp
{
    public int Ammo = 100;
    public float Duration = 10f;
    public Gun GunType;

    public override void OnPickup(Player player)
    {
        player.UpgradeWeapon(GunType, Duration);
        player.AddAmmo(Ammo);
    }
}
