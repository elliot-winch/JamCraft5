using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFunction : I2DFunction
{
    public float Sample(float x, float y)
    {
        return 0f;// Mathf.PerlinNoise(x, y);
    }
}
