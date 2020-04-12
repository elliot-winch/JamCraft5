using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HexGrid : Grid
{
    public enum TopType
    {
        Flat,
        Pointy
    }

    //The Vector2Int incides are axial coordinates, converted to
    //other hex coordinate systems where appropriate

    private TopType mType;
    private float mSize;
    private int mEdgePoints;
    //Hex Grids have three dimensions for their boundary
    private int mDepthBoundaryLeft;
    private int mDepthBoundaryRight;

    private float mSqrt3;
    private Vector2 mOffset;
    private float mScalar;

    public HexGrid(int points, float size, TopType type) : base(points * 2 + 1, points * 2 + 1)
    {
        mType = type;
        mSize = size;
        mEdgePoints = points;
        mDepthBoundaryLeft = -points * 3;
        mDepthBoundaryRight = -points;

        //Constants
        mSqrt3 = Mathf.Sqrt(3f);
    }

    public override bool IsValidIndex(Vector2Int index)
    {
        Vector3Int cube = AxialToCube(index);
        return cube.x >= 0 && cube.x < Width
            && cube.y >= 0 && cube.y < Height
            && cube.z <= mDepthBoundaryRight && cube.z >= mDepthBoundaryLeft;
    }

    protected override Vector2 CalculatePositionForIndex(Vector2 index)
    {
        float normalisedIndexX = index.x;
        float normalisedIndexY = index.y;

        Vector2 position;
        Vector2 offset;

        if(mType == TopType.Pointy)
        {
            position = PointyPosition(normalisedIndexX, normalisedIndexY);
            offset = new Vector2(0.75f, 3 * mSqrt3 / 4f);
        }
        else
        {
            position = FlatPosition(normalisedIndexX, normalisedIndexY);
            offset = new Vector2(3 * mSqrt3 / 4f, 0.75f);
        }

        Vector2 normalisedPosition = new Vector2(position.x / (Width - 1) - offset.x, position.y / (Height - 1) - offset.y);
        return (normalisedPosition) * mSize;
    }

    private Vector2 FlatPosition(float x, float y)
    {
        float newX = mSqrt3 * x + mSqrt3 / 2f * y;
        float newY = 1.5f * y;
        return new Vector2(newX, newY);
    }

    private Vector2 PointyPosition(float x, float y)
    {
        float newX = 1.5f * x;
        float newY = mSqrt3 / 2f * x + mSqrt3 * y;
        return new Vector2(newX, newY);
    }

    private Vector3Int AxialToCube(Vector2Int axial)
    {
        return new Vector3Int()
        {
            x = axial.x,
            y = axial.y,
            z = - axial.x - axial.y
        };
    }

    private Vector2Int CubeToAxial(Vector3Int cube)
    {
        return new Vector2Int()
        {
            x = cube.x,
            y = cube.y
        };
    }
}
