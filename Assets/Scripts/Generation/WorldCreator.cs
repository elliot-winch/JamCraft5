using UnityEngine;

public class WorldCreator : MonoBehaviour
{
    [SerializeField]
    private Mapper mBlendCurve;
    [SerializeField]
    private TileVisuals mVisualsPrefab;
    [SerializeField]
    private WorldMap mMap;

    //Start is temp
    private void Start()
    {
        Grid worldGrid = new Grid(new HexGrid(4, 4 * Mathf.Sqrt(3), HexGrid.TopType.Pointy));
        worldGrid.Init();

        UnityEngine.Profiling.Profiler.BeginSample("Create World Map");
        mMap.CreateWorldModel(worldGrid);
        UnityEngine.Profiling.Profiler.EndSample();

        CreateWorldView(mMap);
    }

    public void CreateWorldView(WorldMap map)
    {
        WorldViewFunction worldFunction = new WorldViewFunction(mBlendCurve);

        UnityEngine.Profiling.Profiler.BeginSample("Create View Function");
        map.Grid.IterateOverPoints((point) =>
        {
            Tile tile = map.TileAt(point);

            worldFunction.AddFunction(point.Position, new TileFunction(tile.BiomeData.ViewGenerationParameters));
        });
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Create Tile Visuals");
        map.Grid.IterateOverPoints((point) =>
        {
            TileVisuals tileVisual = Instantiate(mVisualsPrefab);
            tileVisual.CreateMesh(worldFunction, point.Position);

            tileVisual.transform.position = new Vector3()
            {
                x = point.Position.x,
                z = point.Position.y
            };

            Tile tile = map.TileAt(point);
            tileVisual.Material = tile.BiomeData.Material;
        });
        UnityEngine.Profiling.Profiler.EndSample();
    }
}
