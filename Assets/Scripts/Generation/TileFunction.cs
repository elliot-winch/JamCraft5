using UnityEngine;

public class TileFunction : I2DFunction
{
    private PerlinNoise[] mNoiseFunctions;
    private TaperFunction mTaperFunction;

    public TileFunction(TileFunctionNoiseParameters noiseParameters, TileFunctionTaperParameters taperParameters)
    {
        mNoiseFunctions = new PerlinNoise[noiseParameters.StepCount];

        for(int i = 0; i < noiseParameters.StepCount; i++)
        {
            float stepFactor = noiseParameters.StepCount > 1 ? i / (float)(noiseParameters.StepCount - 1) : 0f;
            float octave = noiseParameters.OctaveCurve.Evaluate(stepFactor) * (noiseParameters.MaxOctave - noiseParameters.MinOctave) + noiseParameters.MinOctave;
            float scale = noiseParameters.ScaleCurve.Evaluate(stepFactor) * (noiseParameters.MaxScale - noiseParameters.MinScale) + noiseParameters.MinScale;

            Debug.Log(string.Format("Step number: {0}, Octave: {1}, Scale: {2}", i, octave, scale));

            mNoiseFunctions[i] = new PerlinNoise(octave, scale, noiseParameters.NoiseType);
        }

        if(taperParameters != null)
        {
            mTaperFunction = new TaperFunction(taperParameters.Center, taperParameters.Radius);
        }
    }

    public float Sample(float x, float y)
    {
        float value = 0f;

        foreach(I2DFunction function in mNoiseFunctions)
        {
            value += function.Sample(x, y);
        }

        return value * (mTaperFunction?.Sample(x, y) ?? 1f);
    }
}
