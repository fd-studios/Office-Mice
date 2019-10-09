using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : PowerUp
{
    public int Heal = 50;

    public Sprite ToastIcon;

    public string Title, Price;

    public override void OnPickup(Player player)
    {
        player.AddHealth(Heal);
        player.ToastPanel.ToastItem(ToastIcon, Title, Price);
    }

    public void Update()
    {
        var y = Mathf.Sin(Time.fixedTime * Mathf.PI * 1f) * 0.01f;

        _transform.position += new Vector3(0, y, 0);
    }
}
