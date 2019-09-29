using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                if (GameOver != null) GameOver.Play();
                return;
            }
        }
        StartCoroutine(_respawn(obj, seconds, callback));
    }

    IEnumerator _respawn(GameObject obj, int seconds, Action callback = null)
    {
        yield return new WaitForSeconds(seconds);
        obj.SetActive(true);
        callback?.Invoke();
        yield break;
    }
}
