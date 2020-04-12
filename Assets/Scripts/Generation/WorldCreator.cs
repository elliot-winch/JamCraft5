using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCreator : MonoBehaviour
{
    [SerializeField]
    private TileVisuals mTileVisualsPrefab;  //for now, just one kind of tile
    [SerializeField]
    private AnimationCurveContainer mBlendCurve;
    [SerializeField]
    private TileFunctionNoiseParameters mNoiseParameters;
    [SerializeField]
    private TileFunctionTaperParameters mTaperParameters;

    private void Start()
    {
        //TEMP
        CreateWorld();
    }

    public void CreateWorld()
    {
        HexGrid tileGrid = new HexGrid(1, Mathf.Sqrt(3), HexGrid.TopType.Pointy);
        tileGrid.Init();

        WorldFunction worldFunction = new WorldFunction(mBlendCurve);

        tileGrid.IterateOverPoints((point) =>
        {
            worldFunction.AddFunction(point.Position, new TileFunction(mNoiseParameters, mTaperParameters));
        });

        tileGrid.IterateOverPoints((point) =>
        {
            TileVisuals tile = Instantiate(mTileVisualsPrefab);
            tile.Create(worldFunction, point.Position);

            tile.transform.position = new Vector3()
            {
                x = point.Position.x,
                z = point.Position.y
            };
        });
    }
}
