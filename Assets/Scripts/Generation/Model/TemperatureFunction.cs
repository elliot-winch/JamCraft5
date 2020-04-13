using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureFunction //: I2DFunction
{
    public TemperatureFunction(LayeredPerlinNoiseParameters noiseParams)
    {
        LayeredPerlinNoise elevationNoise = new LayeredPerlinNoise(noiseParams);
    }
}
