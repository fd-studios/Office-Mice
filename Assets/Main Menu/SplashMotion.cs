using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashMotion : MonoBehaviour
{
    public GameObject Sun, Fd, Shadow;
    public GameObject bgm;

    float _splashStart;
    SpriteRenderer _sunSR, _fdSR, _shadowSR;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(bgm);
        MenuHandlers.runningBgm = bgm;

        _splashStart = Time.realtimeSinceStartup;

        _sunSR = Sun.GetComponent<SpriteRenderer>();
        _fdSR = Fd.GetComponent<SpriteRenderer>();
        _shadowSR = Shadow.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup - _splashStart > 3.5)
            SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);

        var animateTime = 2f;
        if(Time.realtimeSinceStartup - _splashStart < animateTime)
        {
            var dt = Time.deltaTime / animateTime;
            Sun.transform.Translate(new Vector3(0, -.2f * dt, 0));
            Shadow.transform.localScale += new Vector3(0, .5f * dt, 0);

            _sunSR.color = _fdSR.color = _shadowSR.color = new Color(1, _shadowSR.color.g - 1 * dt, _shadowSR.color.b - .4f * dt);
        }
    }
}
