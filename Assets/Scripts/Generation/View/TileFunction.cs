using UnityEngine;

public class TileFunction : I2DFunction
{
    private LayeredPerlinNoise mNoiseFunction;
    private TaperFunction mTaperFunction;

    public TileFunction(LayeredPerlinNoiseParameters noiseParams)
    {
        mNoiseFunction = new LayeredPerlinNoise(noiseParams);
    }

    public TileFunction(LayeredPerlinNoiseParameters noiseParameters, float taperRadius) : this(noiseParameters)
    {
        mTaperFunction = new TaperFunction(Vector2.zero, taperRadius);
    }

    public float Sample(float x, float y)
    {
        return mNoiseFunction.Sample(x, y) * (mTaperFunction?.Sample(x, y) ?? 1f);
    }
}
