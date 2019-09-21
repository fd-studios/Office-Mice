using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandlers : MonoBehaviour
{
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
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
    public void HighScores()
    {
        SceneManager.LoadScene("HighScoresScene", LoadSceneMode.Single);
    }
}
