using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : PowerUp
{
    public int Heal = 50;
    public float SpeedTime = 0;

    public Sprite ToastIcon;

    public string Title, Price;
    public bool Float = true;
    public bool Special = false;


    public override void OnPickup(Player player)
    {
        if(Heal > 0)
            player.AddHealth(Heal);

        if(SpeedTime > 0)
            player.AddSpeed(SpeedTime);

        if (Special)
            player.ToastPanel.Toast(ToastIcon, Price, Title, 4, true);
        else
            player.ToastPanel.ToastItem(ToastIcon, Title, Price);
    }

    public void Update()
    {
        if (Float)
        {
            var y = Mathf.Sin(Time.fixedTime * Mathf.PI * 1f) * 0.01f;

            _transform.position += new Vector3(0, y, 0);
        }
    }
}
