using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandlers : MonoBehaviour
{
    public static GameObject runningBgm;
    public GameObject bgm;

    // Start is called before the first frame update
    void Start()
    {
        if (runningBgm != null)
        {
            Destroy(bgm);
        }
        else
        {
            runningBgm = bgm;
            DontDestroyOnLoad(bgm);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        SceneManager.LoadScene("ControlsScene", LoadSceneMode.Single);
    }

    void GameStart()
    {
        Destroy(runningBgm);
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public void MouseStart()
    {
        PlayerPrefs.SetInt("aim", 0);
        GameStart();
    }

    public void ArrowStart()
    {
        PlayerPrefs.SetInt("aim", 1);
        GameStart();
    }

    public void TouchStart()
    {
        PlayerPrefs.SetInt("aim", 2);
        GameStart();
    }


    public void HighScores()
    {
        SceneManager.LoadScene("HighScoresScene", LoadSceneMode.Single);
    }

    public void About()
    {
        SceneManager.LoadScene("AboutScene", LoadSceneMode.Single);
    }
}
