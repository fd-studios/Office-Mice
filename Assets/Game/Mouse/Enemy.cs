using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class Enemy : MonoBehaviour
{
    public string Name { get; set; }
    public int StatMultiplier { get; set; } = 1;
    public int BaseHealth { get; set; } = 120;
    public float BaseSpeed { get; set; } = 3f;
    public float BaseDamage { get; set; } = 10f;
    public int Health { get; protected set; }
    public float Speed { get; protected set; }
    public bool IsDead { get; set; }

    public virtual void Kill()
    {

    }
}
