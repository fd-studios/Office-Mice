using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesCounter : MonoBehaviour
{
    Game _game;
    Text Label;

    // Start is called before the first frame update
    void Start()
    {
        _game = GameObject.FindObjectOfType<Game>();
        Label = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Label.text = _game.Lives.ToString();
    }
}
