using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCrate : PowerUp
{
    public int Ammo = 30;

    public override void OnPickup(Player player)
    {
        player.AddAmmo(Ammo);
    }
}
