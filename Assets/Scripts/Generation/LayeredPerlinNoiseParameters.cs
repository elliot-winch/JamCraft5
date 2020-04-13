using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tile Function Noise Parameters", menuName = "ScriptableObjects/Layered Perlin Noise Parameters")]
public class LayeredPerlinNoiseParameters : ScriptableObject
{
    public int StepCount;
    public PerlinNoise.NoiseType NoiseType;
    [SerializeField]
    public Mapper Octave;
    [SerializeField]
    public Mapper Scale;
}
