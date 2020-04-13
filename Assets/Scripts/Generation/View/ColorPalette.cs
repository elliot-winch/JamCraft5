using System;
using System.Linq;
using UnityEngine;

public class ColorPalette : ScriptableObject
{
    [Serializable]
    public struct ColorKey
    {
        [SerializeField]
        public Color Color;
        [Range(0,1)]
        public float Key;
    }

    [SerializeField]
    private ColorKey[] mColors;

    public Color Sample(float value)
    {
        Validate();

        for(int i = 0; i < mColors.Length - 1; i++)
        {
            ColorKey colorKey = mColors[i];

            if(value > colorKey.Key)
            {
                ColorKey nextKey = mColors[i + 1];
                float normalisedValue = (value - colorKey.Key) / (nextKey.Key - colorKey.Key);

                return Color.Lerp(colorKey.Color, nextKey.Color, normalisedValue);
            }
        }

        return Color.black;
    }

    private void Validate()
    {
        mColors = mColors.OrderBy(colorKey => colorKey.Key).ToArray();
    }
}
