using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I2DFunction
{
    float Sample(float x, float y);
}

public static class I2DExtension
{
    public static float Sample(this I2DFunction function, Vector2 position)
    {
        return function.Sample(position.x, position.y);
    }
}
