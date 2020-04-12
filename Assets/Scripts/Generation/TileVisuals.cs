using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class TileVisuals : MonoBehaviour
{
    [SerializeField]
    private int mPointDensity = 10;
    [SerializeField]
    private float mSize = 1f;
    [SerializeField]
    private HexGrid.TopType mTopType;

    private MeshFilter mMeshFilter;

    public FunctionMesher Mesher { get; private set; }

    private void Awake()
    {
        mMeshFilter = GetComponent<MeshFilter>();
    }

    public void Create(I2DFunction function, Vector2 sampleOffset)
    {
        Grid grid = new HexGrid(mPointDensity, mSize, mTopType);
        grid.Init();

        Mesher = new FunctionMesher(grid);

        mMeshFilter.mesh = Mesher.ApplyFunction(function, sampleOffset);
    }
}
