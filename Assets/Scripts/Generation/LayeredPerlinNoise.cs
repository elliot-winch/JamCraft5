using UnityEngine;

public class LayeredPerlinNoise : I2DFunction
{
    private PerlinNoise[] mNoiseFunctions;

    public LayeredPerlinNoise(LayeredPerlinNoiseParameters noiseParameters)
    {
        mNoiseFunctions = new PerlinNoise[noiseParameters.StepCount];

        for(int i = 0; i < noiseParameters.StepCount; i++)
        {
            float stepFactor = noiseParameters.StepCount > 1 ? i / (float)(noiseParameters.StepCount - 1) : 0f;
            float octave = noiseParameters.Octave.Resolve(stepFactor);
            float scale = noiseParameters.Scale.Resolve(stepFactor);

            //Debug.Log(string.Format("Step number: {0}, Octave: {1}, Scale: {2}", i, octave, scale));

            mNoiseFunctions[i] = new PerlinNoise(octave, scale, noiseParameters.NoiseType);
        }
    }

    public float Sample(float x, float y)
    {
        float value = 0f;

        foreach(I2DFunction function in mNoiseFunctions)
        {
            value += function.Sample(x, y);
        }

        return value;
    }
}
