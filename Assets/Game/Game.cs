using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public uint Lives = 3;
    public AudioSource GameOver;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Respwan(GameObject obj, int seconds, Action callback = null)
    {
        if(obj.tag == "Player")
        {
            if (Lives > 0)
            {
                Lives--;
            }
            else
            {
                GameOver.Play();
                StartCoroutine(GameOverEvent(seconds));
                return;
            }
        }

        if(seconds > 0)
            StartCoroutine(_respawn(obj, seconds, callback));
    }

    IEnumerator _respawn(GameObject obj, int seconds, Action callback = null)
    {
        yield return new WaitForSeconds(seconds);
        obj.SetActive(true);
        callback?.Invoke();
        yield break;
    }

    IEnumerator GameOverEvent(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("GameOverScene", LoadSceneMode.Single);
        yield break;
    }
}
