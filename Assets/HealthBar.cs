using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    Player _player;
    RectTransform _rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindObjectOfType<Player>();
        _rectTransform = gameObject.GetComponent<RectTransform> ();
    }

    // Update is called once per frame
    void Update()
    {
        float health = _player.Health;
        float baseHealth = _player.BaseHealth;
        _rectTransform.sizeDelta = new Vector2(150f * health/baseHealth, _rectTransform.sizeDelta.y);
    }
}
