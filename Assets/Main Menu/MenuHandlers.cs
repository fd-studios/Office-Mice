using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandlers : MonoBehaviour
{
    public static GameObject runningBgm;

    public Dropdown AimDropdown;

    // Start is called before the first frame update
    void Start()
    {
        AimDropdown.value = PlayerPrefs.GetInt("aim");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        PlayerPrefs.SetInt("aim", AimDropdown.value);

        Destroy(runningBgm);
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
    public void HighScores()
    {
        SceneManager.LoadScene("HighScoresScene", LoadSceneMode.Single);
    }
}
