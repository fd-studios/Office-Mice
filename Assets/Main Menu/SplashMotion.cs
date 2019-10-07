using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashMotion : MonoBehaviour
{
    public Transform Sun, Shadow;
    public GameObject bgm;

    float _splashStart;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(bgm);
        MenuHandlers.runningBgm = bgm;

        _splashStart = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup - _splashStart > 3.5)
            SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);

        if(Time.realtimeSinceStartup - _splashStart < 2)
        {
            Sun.Translate(new Vector3(0, -.1f / 2 * Time.deltaTime, 0));
            Shadow.localScale += new Vector3(0, -.5f / 2 * Time.deltaTime, 0);
        }
    }
}
