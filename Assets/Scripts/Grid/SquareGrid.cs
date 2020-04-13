using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectangluarGrid : IGridPositioner
{
    public int Width { get; private set; }
    public int Height { get; private set; }

    public RectangluarGrid(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public bool IsValidIndex(Vector2Int index)
    {
        return index.x >= 0 && index.x < Width && index.y >= 0 && index.y < Height;
    }

    public Vector2 CalculatePositionForIndex(Vector2 index)
    {
        return new Vector2(index.x / (Width - 1), index.y / (Height - 1));
    }
}
