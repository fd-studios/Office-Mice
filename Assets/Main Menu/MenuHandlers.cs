using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandlers : MonoBehaviour
{
    public static GameObject runningBgm;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        SceneManager.LoadScene("ControlsScene", LoadSceneMode.Single);
    }

    public void MouseStart()
    {
        PlayerPrefs.SetInt("aim", 0);

        Destroy(runningBgm);
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public void ArrowStart()
    {
        PlayerPrefs.SetInt("aim", 1);

        Destroy(runningBgm);
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }


    public void HighScores()
    {
        SceneManager.LoadScene("HighScoresScene", LoadSceneMode.Single);
    }
}
