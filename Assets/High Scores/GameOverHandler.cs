using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverHandler : MonoBehaviour
{
    public Text ScoreText;
    public InputField PlayerName;

    public GameObject bgm;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("name"))
            PlayerName.text = PlayerPrefs.GetString("name");
        ScoreText.text = PlayerPrefs.GetInt("score").ToString();

        MenuHandlers.runningBgm = bgm;
        DontDestroyOnLoad(bgm);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Submit()
    {
        PlayerPrefs.SetString("name", PlayerName.text);
        SceneManager.LoadScene("HighScoresScene", LoadSceneMode.Single);
    }
}
