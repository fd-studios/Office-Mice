using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : PowerUp
{
    public int Heal = 50;

    public override void OnPickup(Player player)
    {
        player.AddHealth(Heal);
    }
}
