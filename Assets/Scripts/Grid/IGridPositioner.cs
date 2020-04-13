using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGridPositioner 
{
    int Width { get; }
    int Height { get; }
    bool IsValidIndex(Vector2Int index);
    Vector2 CalculatePositionForIndex(Vector2 index);
}
