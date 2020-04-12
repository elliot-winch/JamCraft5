using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Linear, for now
public class TaperFunction : I2DFunction
{
    private Vector2 mCenterPosition;
    private float mZeroDistance;

    /// <param name="zeroDistance">The distance at which a point will be zero</param>
    public TaperFunction(Vector2 centerPosition, float zeroDistance)
    {
        mCenterPosition = centerPosition;
        mZeroDistance = zeroDistance;
    }

    public float Sample(float x, float y)
    {
        float distance = Vector2.Distance(new Vector2(x, y), mCenterPosition);
        return Mathf.Max(0f, 1 - distance / mZeroDistance);
    }
}
