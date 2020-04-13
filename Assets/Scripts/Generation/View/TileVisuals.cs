using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class TileVisuals : MonoBehaviour
{
    [SerializeField]
    private int mPointDensity = 10;
    [SerializeField]
    private float mSize = 1f;
    [SerializeField]
    private HexGrid.TopType mTopType;

    private MeshFilter mMeshFilter;
    private MeshRenderer mMeshRenderer;

    public FunctionMesher Mesher { get; private set; }

    public Material Material
    {
        get => mMeshRenderer.material;
        set => mMeshRenderer.material = value;
    }

    private void Awake()
    {
        mMeshFilter = GetComponent<MeshFilter>();
        mMeshRenderer = GetComponent<MeshRenderer>();
    }

    public void CreateMesh(I2DFunction function, Vector2 sampleOffset)
    {
        Grid grid = new Grid(new HexGrid(mPointDensity, mSize, mTopType));
        grid.Init();

        Mesher = new FunctionMesher(grid);

        mMeshFilter.mesh = Mesher.ApplyHeightFunction(function, sampleOffset);
    }
}
