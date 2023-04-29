using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FloatExtensions
{
    public static float GetPercentageBetween(this float value, float min, float max)
    {
        if (value < min) return 0f;
        if (value > max) return 1f;
        return (value - min) / (max - min);
    }
    public static float GetValueFromPercentage(this float percentage, float min, float max)
    {
        if (percentage <= 0f) return min;
        if (percentage >= 1f) return max;
        return min + (max - min) * percentage;
    }
}
