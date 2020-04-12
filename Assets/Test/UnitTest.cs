﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTest : MonoBehaviour
{
    public FunctionMesher mVisualsPrefabA;
    public FunctionMesher mVisualsPrefabB;
    [SerializeField]
    public AnimationCurveContainer mBlendCurve;

    [Header("Test Tile")]
//These are the parameters a factory might generate / load from a file
//Fow now, we're just setting them manually
    [SerializeField]
    private int mPointDensity = 10;
    [SerializeField]
    private float mSize = 1f;
    [SerializeField]
    private HexGrid.TopType mTopType;
    [SerializeField]
    private TileFunctionNoiseParameters mNoiseParameters;
    [SerializeField]
    private TileFunctionTaperParameters mTaperParameters;


    private void Start()
    {
        FunctionMesher mA = Test_Mesh(mVisualsPrefabA);

        I2DFunction function = new TileFunction(mNoiseParameters, mTaperParameters);
        FunctionMesher mB = Test_Mesh(mVisualsPrefabB, function);
        FunctionMesher mBImmutable = Test_Mesh(mVisualsPrefabB, function);

        mA.name = "A";
        mB.name = "B";
        mBImmutable.name = "BOriginial";
        mBImmutable.transform.position = Vector3.one * 2f;

        Vector3 position = new Vector3(Mathf.Sqrt(3) * 3/4f, 0f, 3/4f);
        mA.transform.position = position;

        Vector3 difference = mB.transform.position - mA.transform.position;

        mB.BlendMesh(mA.Function, -new Vector2(difference.x, difference.z), mBlendCurve);
        //mA.BlendMesh(mB.Function, - new Vector2(difference.x, difference.z), mBlendCurve);
    }

    private FunctionMesher Test_Mesh(FunctionMesher prefab, I2DFunction func = null)
    {
        var tileVisual = Instantiate(prefab);

        Grid grid = new HexGrid(mPointDensity, mSize, mTopType);
        grid.Init();

        I2DFunction function = func ?? new TileFunction(mNoiseParameters, mTaperParameters);

        tileVisual.CreateMesh(function, grid);

        return tileVisual;
    }

    private void Tile_Grid_Test()
    {
        HexGrid tileGrid = new HexGrid(1, Mathf.Sqrt(3), HexGrid.TopType.Pointy);
        tileGrid.Init();

        I2DFunction function = new TileFunction(mNoiseParameters, mTaperParameters);

        tileGrid.IterateOverPoints((tile) =>
        {
            var tileVisual = Instantiate(mVisualsPrefabA);

            Grid grid = new HexGrid(mPointDensity, mSize, mTopType);
            grid.Init();

            tileVisual.CreateMesh(function, grid, tile.Position);

            tileVisual.transform.position = new Vector3()
            {
                x = tile.Position.x,
                y = 0f,
                z = tile.Position.y
            };
        });
        
    }
}
