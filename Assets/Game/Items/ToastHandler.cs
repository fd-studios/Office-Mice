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
    
    /// <summary>
    /// current toast should not be replaced
    /// </summary>
    bool _important = false;

    public void ToastWeaponUpgrade(Sprite sprite)
    {
        ToastItem(sprite, "N-Strike Elite SurgeFire", "$9.99", 3);
    }

    public void ToastWeaponDowngrade()
    {
        ToastItem(weapon, "N-Strike Elite Disruptor", "$9.99", 3);
    }

    public void ToastAmmo()
    {
        ToastItem(ammo, "N-Strike Elite Ammo", "$9.99");
    }
    public void ToastItem(Sprite sprite, string title, string price, float time = 2)
    {
        Toast(sprite, "You got:", $"{title}{Environment.NewLine}{price}", time);
    }

    public void Toast(Sprite sprite, string title, string content, float time = 2, bool important = false)
    {
        if (_important && !important) return;
        _important = important;

        Panel.SetActive(true);
        Title.text = title;
        Content.text = content;
        Image.sprite = sprite;
        _timeOut = time;
    }

    // Update is called once per frame
    void Update()
    {
        if (_timeOut > 0)
        {
            _timeOut -= Time.deltaTime;
            if(_timeOut <= 0)
            {
                Panel.SetActive(false); 
                _important = false;
            }
        }
    }
}
