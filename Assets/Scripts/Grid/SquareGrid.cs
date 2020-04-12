using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareGrid : Grid
{
    public SquareGrid(int width, int height) : base(width, height) { }

    public override bool IsValidIndex(Vector2Int index)
    {
        return index.x >= 0 && index.x < Width && index.y >= 0 && index.y < Height;
    }

    protected override Vector2 CalculatePositionForIndex(Vector2 index)
    {
        return new Vector2(index.x / (Width - 1), index.y / (Height - 1));
    }
}
