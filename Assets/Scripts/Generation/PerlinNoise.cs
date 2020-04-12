using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise : I2DFunction
{
    public enum NoiseType
    {
        Smooth,
        Ridged
    }

    private const float HALF_SEED_RANGE = 10000f;

    private Vector2 mSeed;
    private Vector2 mOctave;
    private float mScale;
    private NoiseType mType;

    public PerlinNoise(float octave, float scale, NoiseType type) : this(octave * Vector2.one, scale, type) { }

    public PerlinNoise(Vector2 octave, float scale, NoiseType type)
    {
        mSeed = new Vector2(Random.Range(-HALF_SEED_RANGE, HALF_SEED_RANGE), Random.Range(-HALF_SEED_RANGE, HALF_SEED_RANGE));
        mOctave = octave;
        mScale = scale;
        mType = type;
    }

    public float Sample(float x, float y)
    {
        float xPosition = (x + mSeed.x) * mOctave.x;
        float yPosition = (y + mSeed.y) * mOctave.y;

        float noise = Mathf.PerlinNoise(xPosition, yPosition);

        if(mType == NoiseType.Ridged)
        {
            //Sink so midpoint is zero (Perlin noise varies from 0 to 1)
            noise -= 0.5f;
            noise = -Mathf.Abs(noise);
            noise += 0.5f;
        }

        return noise * mScale;
    }

    //Operates in place
    /*
    protected void AddPerlinNoise(List<NavigablePoint> points, PerlinNoiseIterationArgs args, NoiseGenerationType genType = NoiseGenerationType.Smooth, Vector2? seed = null)
    {
        float octave = args.startingOctave;
        float res = args.startingScale;

        for (int i = 0; i < args.iterations; i++)
        {
            AddPerlinNoise(points, octave, res, genType);

            octave *= args.octaveIterationModifier;
            res *= args.scaleIterationModifier;
        }
    }
    */

    //Operates in place
}
