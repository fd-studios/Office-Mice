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

    public void Update()
    {
        var y = Mathf.Sin(Time.fixedTime * Mathf.PI * 1f) * 0.01f;

        _transform.position += new Vector3(0, y, 0);
    }
}
