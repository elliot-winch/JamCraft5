using UnityEngine;

public class WorldMap : MonoBehaviour
{
    [SerializeField]
    private float mSeaLevel;
    [SerializeField]
    private Biome mOceanBiomeData;
    [SerializeField]
    private WhittakerTable mWhittakerTable;
    [SerializeField]
    private LayeredPerlinNoiseParameters mElevationNoiseParams;
    [SerializeField]
    private LayeredPerlinNoiseParameters mPrecipitationNoiseParams;

    public Grid Grid { get; private set; }
    public Tile[,] Tiles { get; private set; }

    public void CreateWorldModel(Grid worldGrid)
    {
        Grid = worldGrid;
        Tiles = new Tile[worldGrid.Width, worldGrid.Height];

        LayeredPerlinNoise elevationFunction = new LayeredPerlinNoise(mElevationNoiseParams);
        LayeredPerlinNoise precipitationFunction = new LayeredPerlinNoise(mPrecipitationNoiseParams);

        worldGrid.IterateOverPoints((point) =>
        {
            float elevation = elevationFunction.Sample(point.Position);
            //TODO use latitude to convert elevation to temperature
            float precipation = precipitationFunction.Sample(point.Position);

            Biome biomeData = DetermineBiome(elevation, precipation);

            //Debug.LogFormat("El: {0}, Pre: {1}, Biome: {2}", elevation, precipation, biomeData.Name);

            Tiles[point.GridIndex.x, point.GridIndex.y] = new Tile()
            {
                BiomeData = biomeData,
                Precipitation = precipation,
                Elevation = elevation
            };
        });
    }

    public Tile TileAt(Grid.Point point)
    {
        return Tiles[point.GridIndex.x, point.GridIndex.y];
    }

    private Biome DetermineBiome(float elevation, float precipitation)
    {
        if (elevation < mSeaLevel)
        {
            return mOceanBiomeData;
        }

        int elevationIndex = Mathf.FloorToInt(elevation * mWhittakerTable.Rows.Count);
        int precipitationIndex = Mathf.FloorToInt(precipitation * mWhittakerTable.Rows[elevationIndex].Data.Count);

        return mWhittakerTable.Rows[elevationIndex].Data[precipitationIndex];
    }
}
