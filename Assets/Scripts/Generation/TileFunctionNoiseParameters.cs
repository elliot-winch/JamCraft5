using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tile Function Noise Parameters", menuName = "ScriptableObjects/Tile Function Noise Parameters")]
public class TileFunctionNoiseParameters : ScriptableObject
{
    public int StepCount;
    public PerlinNoise.NoiseType NoiseType;
    [Header("Octave")]
    public float MinOctave;
    public float MaxOctave;
    public AnimationCurve OctaveCurve;
    [Header("Scale")]
    public float MinScale;
    public float MaxScale;
    public AnimationCurve ScaleCurve;
}
