using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    Game _game;

    protected Transform _transform;
    protected SpriteRenderer _sprite;
    public int RespawnDelay = 60;

    public abstract void OnPickup(Player player);

    protected virtual void UpdateItem() { }

    private void Start()
    {
        _game = FindObjectOfType<Game>();
        _transform = GetComponent<Transform>();
    }

    private void OnEnable()
    {
        _sprite = GetComponent<SpriteRenderer>();
        UpdateItem();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameObject.SetActive(false);
            Player player = collision.GetComponent<Player>();
            OnPickup(player);
            _game.Respwan(gameObject, RespawnDelay);
        }
    }
}
