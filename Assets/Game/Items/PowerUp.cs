using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    Game _game;

    protected Transform _transform;
    public int RespawnDelay = 60;

    public abstract void OnPickup(Player player);

    private void Start()
    {
        _game = GameObject.FindObjectOfType<Game>();
        _transform = GetComponent<Transform>();
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
