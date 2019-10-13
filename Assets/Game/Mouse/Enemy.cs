using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class Enemy : MonoBehaviour
{
    public string Name;
    public int StatMultiplier { get; set; } = 1;
    public int BaseHealth = 120;
    public float BaseSpeed = 3f;
    public float BaseDamage = 10f;
    public float BaseRushIncrement = 3f;
    public float BasePlayerDetectionDistance = 3f;
    public float MaxSpeed = 20f;
    public int Health { get; protected set; }
    public float Speed { get; protected set; }
    public float Damage { get; set; }
    public float RushIncrement { get; set; }
    public bool IsDead { get; set; }
    public float PlayerDetectionDistance { get; set; }

public virtual void Kill()
    {

    }
}
