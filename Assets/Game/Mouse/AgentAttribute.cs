using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
[DebuggerDisplay("Value = {current}")]
public struct AgentAttribute : IEquatable<AgentAttribute>, IEquatable<float>
{
    public float BaseValue;
    public float MaxValue;
    public float MinValue;
    //Used to determine how many waves it take to double the value
    public float Increment;
    private float current;

    private AgentAttribute(float baseValue, float minValue, float maxValue, float increment, float value)
    {
        BaseValue = baseValue;
        MinValue = minValue;
        MaxValue = maxValue;
        Increment = increment;
        current = value;
    }

    private AgentAttribute(AgentAttribute attribute, float value)
        : this(attribute.BaseValue, attribute.MinValue, attribute.MaxValue, attribute.Increment, value)
    {
    }

    public float GetCurrent()
    {
        return current;
    }

    public void Reset(int multiplier)
    {
        current = Mathf.Min(BaseValue * (1 + (multiplier - 1) / (Increment > 0 ? Increment : 1)), MaxValue);
    }

    public static AgentAttribute operator +(AgentAttribute left, float right)
    {
        var value = Mathf.Min(left.current + right, left.MaxValue);
        return new AgentAttribute(left, value);
    }

    public static AgentAttribute operator -(AgentAttribute left, float right)
    {
        var value = Mathf.Max(left.current - right, left.MinValue);
        return new AgentAttribute(left, value);
    }

    public static bool operator >(AgentAttribute left, AgentAttribute right)
    {
        return left.current > right.current;
    }

    public static bool operator <(AgentAttribute left, AgentAttribute right)
    {
        return left.current < right.current;
    }

    public static bool operator >=(AgentAttribute left, AgentAttribute right)
    {
        return left.current >= right.current;
    }

    public static bool operator <=(AgentAttribute left, AgentAttribute right)
    {
        return left.current <= right.current;
    }

    public static bool operator >(AgentAttribute left, float right)
    {
        return left.current > right;
    }

    public static bool operator <(AgentAttribute left, float right)
    {
        return left.current < right;
    }

    public static bool operator >=(AgentAttribute left, float right)
    {
        return left.current >= right;
    }

    public static bool operator <=(AgentAttribute left, float right)
    {
        return left.current <= right;
    }

    public static bool operator >(float left, AgentAttribute right)
    {
        return left > right.current;
    }

    public static bool operator <(float left, AgentAttribute right)
    {
        return left < right.current;
    }

    public static bool operator >=(float left, AgentAttribute right)
    {
        return left > right.current;
    }

    public static bool operator <=(float left, AgentAttribute right)
    {
        return left < right.current;
    }

    public static implicit operator float(AgentAttribute value)
    {
        return value.current;
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }
        if (obj is AgentAttribute)
        {
            return Equals((AgentAttribute)obj);
        }
        if (obj is float)
        {
            return Equals((float)obj);
        }
        return current.Equals(obj);
    }

    public override int GetHashCode()
    {
        return current.GetHashCode();
    }

    public bool Equals(AgentAttribute other)
    {
        return current == other.current;
    }

    public bool Equals(float other)
    {
        return current == other;
    }

    public override string ToString()
    {
        return $"baseValue:{this.BaseValue};minValue:{this.MinValue};maxValue:{this.MaxValue};increment:{this.Increment};value:{this.current}";
    }
}
