using System;
using UnityEngine;

public class Grid
{
    public class Point
    {
        public int LinearIndex;
        public Vector2Int GridIndex;
        public Vector2 Position;
    }

    public int PointCount => mPositioner.Width * mPositioner.Height;
    public int Width => mPositioner.Width;
    public int Height => mPositioner.Height;

    public Point[,] Points { get; private set; }

    private IGridPositioner mPositioner;

    public Grid(IGridPositioner positioner)
    {
        mPositioner = positioner;
    }

    public void Init()
    {
        Points = new Point[mPositioner.Width, mPositioner.Height];

        for (int i = 0; i < mPositioner.Width; i++)
        {
            for (int j = 0; j < mPositioner.Height; j++)
            {
                Vector2Int index = new Vector2Int(i, j);

                if (mPositioner.IsValidIndex(index))
                {
                    Points[i, j] = new Point()
                    {
                        GridIndex = index,
                        Position = mPositioner.CalculatePositionForIndex(index)
                    };
                }
            }
        }
    }

    public Point PointAt(Vector2Int index)
    {
        if (mPositioner.IsValidIndex(index))
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

        for (int i = 0; i < mPositioner.Width; i++)
        {
            for (int j = 0; j < mPositioner.Height; j++)
            {
                if(mPositioner.IsValidIndex(new Vector2Int(i, j)))
                {
                    operation.Invoke(Points[i, j]);
                }
            }
        }
    }
}