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
            if (se.WaitingSeconds < -se.ExpireSeconds) continue;

            se.WaitingSeconds -= Time.deltaTime;
            if (se.WaitingSeconds < 0 && se.WaitingSeconds + Time.deltaTime >= 0)
            {
                ToastHandler.Toast(se.Image, se.Title, se.Message, 5, true);
                foreach (var item in se.Items)
                    item.SetActive(true);
            }
            else if (se.WaitingSeconds < -se.ExpireSeconds)
            {
                var wasActive = false;
                foreach (var item in se.Items)
                    if (item.activeSelf)
                    {
                        wasActive = true;
                        item.SetActive(false);
                    }

                if (wasActive)
                    ToastHandler.Toast(se.Image, se.Title, se.ExpireMessage, 3, false);

                if (se.RespawnSeconds > 0)
                    se.WaitingSeconds = se.RespawnSeconds;
            }
        }
    }
}

[Serializable]
public class SpecialEvent
{
    public float WaitingSeconds;
    public float ExpireSeconds;
    public float RespawnSeconds;
    public Sprite Image;
    public string Title;
    public string Message;
    public string ExpireMessage;
    public GameObject[] Items;
}