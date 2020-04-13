using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldViewFunction : I2DFunction
{
    private struct PlacedFunction
    {
        public Vector2 Position;
        public I2DFunction Function;
    }

    private List<PlacedFunction> mPlacedFunctions = new List<PlacedFunction>();
    private Mapper mBlendCurve;

    public WorldViewFunction(Mapper blendCurve)
    {
        mBlendCurve = blendCurve;
    }

    public void AddFunction(Vector2 position, I2DFunction function)
    {
        mPlacedFunctions.Add(new PlacedFunction()
        {
            Position = position,
            Function = function
        });
    }

    public float Sample(float x, float y)
    {
        Vector2 samplePosition = new Vector2(x, y);

        //Find the total blend factor and how much each function contributed to this blend
        float totalBlendFactor = 0f;
        Dictionary<PlacedFunction, float> blendFactors = new Dictionary<PlacedFunction, float>();

        for(int i = 0; i < mPlacedFunctions.Count; i++)
        {
            PlacedFunction placedFunction = mPlacedFunctions[i];
            
            float distance = Vector2.Distance(samplePosition, placedFunction.Position);
            float blendFactor = mBlendCurve.Resolve(distance);

            if(blendFactor > 0)
            {
                blendFactors[placedFunction] = blendFactor;
                totalBlendFactor += blendFactor;
            }
        }

        //Find the height with the normalised blend factors
        float height = 0f;
        foreach (var functionFactors in blendFactors)
        {
            float blendFunctionValue = functionFactors.Key.Function.Sample(samplePosition - functionFactors.Key.Position);

            height += (functionFactors.Value / totalBlendFactor) * blendFunctionValue;
        }

        return height;
    }
}
