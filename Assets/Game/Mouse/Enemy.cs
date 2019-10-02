using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string Name { get; set; }
    public uint StatMultiplier { get; set; } = 1;
    public int BaseHealth { get; set; } = 120;
    public float BaseSpeed { get; set; } = 5f;
    public int Health { get; protected set; }
    public float Speed { get; protected set; }

    public virtual void Kill()
    {

    }
}
