using System;
using UnityEngine;

public abstract class Grid
{
    public class Point
    {
        public int LinearIndex;
        public Vector2Int GridIndex;
        public Vector2 Position;
    }

    public int Width { get; private set; }
    public int Height { get; private set; }

    public int PointCount => Width * Height;
    public Point[,] Points { get; private set; }

    public Grid(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public void Init()
    {
        Points = new Point[Width, Height];

        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                Vector2Int index = new Vector2Int(i, j);

                if (IsValidIndex(index))
                {
                    Points[i, j] = new Point()
                    {
                        GridIndex = index,
                        Position = CalculatePositionForIndex(index)
                    };
                }
            }
        }
    }

    public Point PointAt(Vector2Int index)
    {
        if (IsValidIndex(index))
        {
            return Points[index.x, index.y];
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Apply some operation to every individual point
    /// </summary>
    /// <param name="operation">An operation which takes an index as its input</param>
    public void IterateOverPoints(Action<Point> operation)
    {
        if(operation == null)
        {
            throw new ArgumentException("Provided operation is null");
        }

        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                if(IsValidIndex(new Vector2Int(i, j)))
                {
                    operation.Invoke(Points[i, j]);
                }
            }
        }
    }

    public abstract bool IsValidIndex(Vector2Int index);
    protected abstract Vector2 CalculatePositionForIndex(Vector2 index);
}