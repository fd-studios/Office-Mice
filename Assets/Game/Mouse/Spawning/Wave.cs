using System;
using UnityEngine;

[Serializable]
public class Wave
{
    public string Name;
    public Enemy Enemy;
    public int Count;
    public float Rate;
    public int RushTimer;
    public string ObjectTag;
}
