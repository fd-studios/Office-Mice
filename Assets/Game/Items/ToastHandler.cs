using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ToastHandler : MonoBehaviour
{
    public Text Title;
    public Text Content;
    public Image Image;
    public GameObject Panel;

    public Sprite ammo, weapon;

    // Start is called before the first frame update
    void Start()
    {
        Panel.SetActive(false);
    }

    int _toastStack = 0;

    async Task ToastEnable(int delayMs)
    {
        _toastStack++;
        Panel.SetActive(true);

        await Task.Delay(delayMs);

        _toastStack--;
        if (_toastStack == 0)
            Panel.SetActive(false);
    }

    public async Task ToastWeaponUpgrade()
    {
        Title.text = "You got:";
        Content.text = $"N-Strike Elite Disruptor{Environment.NewLine}$9.99";
        Image.sprite = weapon;

        await ToastEnable(3000);
    }

    public async Task ToastAmmo()
    {
        Title.text = "You got:";
        Content.text = $"N-Strike Elite Ammo{Environment.NewLine}$9.99";
        Image.sprite = ammo;

        await ToastEnable(2000);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
