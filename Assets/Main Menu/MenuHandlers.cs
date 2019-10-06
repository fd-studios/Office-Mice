using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandlers : MonoBehaviour
{
    public GameObject bgm;

    public static GameObject runningBgm;

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
        Destroy(runningBgm);
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
    public void HighScores()
    {
        SceneManager.LoadScene("HighScoresScene", LoadSceneMode.Single);
    }
}
