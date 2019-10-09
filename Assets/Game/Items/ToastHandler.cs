using System;
using UnityEngine;
using UnityEngine.UI;

public class ToastHandler : MonoBehaviour
{
    public Text Title;
    public Text Content;
    public Image Image;
    public GameObject Panel;

    public Sprite ammo, weapon, weaponUpgrade;

    // Start is called before the first frame update
    void Start()
    {
        Panel.SetActive(false);
    }

    float _timeOut = 0f;

    void ToastEnable(float delaySeconds)
    {
        Panel.SetActive(true);

        _timeOut = delaySeconds;
    }

    public void ToastWeaponUpgrade(Sprite sprite)
    {
        Title.text = "You got:";
        Content.text = $"N-Strike Elite SurgeFire{Environment.NewLine}$18.88";
        Image.sprite = sprite;

        ToastEnable(3);
    }

    public void ToastWeaponDowngrade()
    {
        Title.text = "You got:";
        Content.text = $"N-Strike Elite Disruptor{Environment.NewLine}$9.99";
        Image.sprite = weapon;

        ToastEnable(3);
    }

    public void ToastAmmo()
    {
        Title.text = "You got:";
        Content.text = $"N-Strike Elite Ammo{Environment.NewLine}$9.99";
        Image.sprite = ammo;

        ToastEnable(2);
    }

    public void ToastItem(Sprite sprite, string title, string price)
    {
        Title.text = "You got:";
        Content.text = $"{title}{Environment.NewLine}{price}";
        Image.sprite = sprite;

        ToastEnable(2);
    }

    // Update is called once per frame
    void Update()
    {
        if (_timeOut > 0)
        {
            _timeOut -= Time.deltaTime;
            if(_timeOut <= 0)
                Panel.SetActive(false);
        }
    }
}
