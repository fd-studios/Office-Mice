using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Specials : MonoBehaviour
{
    public SpecialEvent[] SpecialEvents;
    
    public ToastHandler ToastHandler;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var se in SpecialEvents)
            foreach (var item in se.Items)
                item.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var se in SpecialEvents)
        {
            if (se.StartSeconds < -se.ExpireSeconds) continue;

            se.StartSeconds -= Time.deltaTime;
            if (se.StartSeconds < 0 && se.StartSeconds + Time.deltaTime >= 0)
            {
                ToastHandler.Toast(se.Image, se.Title, se.Message, 4, true);
                foreach (var item in se.Items)
                    item.SetActive(true);
            }
            else if (se.StartSeconds < -se.ExpireSeconds)
            {
                var wasActive = false;
                foreach (var item in se.Items)
                    if (item.activeSelf)
                    {
                        wasActive = true;
                        item.SetActive(false);
                    }

                if(wasActive)
                    ToastHandler.Toast(se.Image, se.Title, se.ExpireMessage, 3, false);
            }
        }
    }
}

[Serializable]
public class SpecialEvent
{
    public float StartSeconds;
    public float ExpireSeconds;
    public Sprite Image;
    public string Title;
    public string Message;
    public string ExpireMessage;
    public GameObject[] Items;
}