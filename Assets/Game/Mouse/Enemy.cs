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
    public AgentAttribute Speed;
    public AgentAttribute Health;
    public AgentAttribute Damage;
    public AgentAttribute RushIncrement;
    public AgentAttribute PlayerDetectionDistance;
    public bool IsDead { get; set; }

    protected virtual void ResetStats()
    {
        Speed.Reset(StatMultiplier);
        Damage.Reset(StatMultiplier);
        Health.Reset(StatMultiplier);
        RushIncrement.Reset(StatMultiplier);
        PlayerDetectionDistance.Reset(StatMultiplier);
        IsDead = false;
    }

    public virtual void Kill()
    {

    }
}
