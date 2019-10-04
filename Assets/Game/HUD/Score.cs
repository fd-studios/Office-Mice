using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    int _score = 0;
    public Text Label;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Label.text = _score.ToString();
        PlayerPrefs.SetInt("score", _score);
    }

    public void IncreaseScore(int amount)
    {
        _score += amount;
    }
}
