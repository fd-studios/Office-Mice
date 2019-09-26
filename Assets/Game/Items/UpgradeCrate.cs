using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCrate : AmmoCrate
{
    public float Duration = 10f;

    public override void OnPickup(Player player)
    {
        base.OnPickup(player);
        player.UpgradeWeapon(Duration);
    }
}
