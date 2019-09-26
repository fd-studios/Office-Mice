using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    Player _player;
    public Text _label;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindObjectOfType<Player>();
        _label = gameObject.GetComponent<Text>();
    }

    void Update()
    {
        _label.text = _player.Ammo.ToString();
    }
}
