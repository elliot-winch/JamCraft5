using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCurveContainer : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve mCurve;
    [SerializeField]
    private float mMinInput = 0f;
    [SerializeField]
    private float mMaxInput = 1f;
    [SerializeField]
    private float mMinOutput = 0f;
    [SerializeField]
    private float mMaxOutput = 1f;

    public float Resolve(float input)
    {
        return ResolveNormalised((input - mMinInput) / (mMaxInput - mMinInput));
    }

    public float ResolveNormalised(float input)
    {
        return mCurve.Evaluate(input) * (mMaxOutput - mMinOutput) + mMinOutput;
    }
}
