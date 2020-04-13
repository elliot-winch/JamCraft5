using System;
using UnityEngine;

[Serializable]
public class Mapper
{
    [SerializeField]
    private AnimationCurve mCurve;
    public float MinInput = 0f;
    public float MaxInput = 1f;
    public float MinOutput = 0f;
    public float MaxOutput = 1f;

    public float Resolve(float input)
    {
        return ResolveNormalised((input - MinInput) / (MaxInput - MinInput));
    }

    public float ResolveNormalised(float input)
    {
        return mCurve.Evaluate(input) * (MaxOutput - MinOutput) + MinOutput;
    }
}
