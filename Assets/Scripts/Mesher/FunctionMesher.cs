using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class FunctionMesher : MonoBehaviour
{
    private MeshFilter mMeshFilter;
    private Grid mGrid;

    public I2DFunction Function { get; private set; }

    private void Awake()
    {
        mMeshFilter = GetComponent<MeshFilter>();
    }

    public void CreateMesh(I2DFunction function, Grid grid)
    {
        CreateMesh(function, grid, Vector2.zero);
    }

    public void CreateMesh(I2DFunction function, Grid grid, Vector2 sampleOffset)
    {
        Function = function;
        mGrid = grid;

        Mesh mesh = BuildMesh(function, grid, sampleOffset);

        mMeshFilter.mesh = mesh;
    }

    public void BlendMesh(I2DFunction blendFunction, Vector2 center, AnimationCurveContainer curve)
    {
        List<Vector3> vertices = new List<Vector3>();

        mGrid.IterateOverPoints((point) =>
        {
            float distance = Vector2.Distance(point.Position, center);
            float blendFactor = curve.Resolve(distance);

            float blendFunctionValue = blendFunction.Sample(point.Position - center);
            float functionValue = Function.Sample(point.Position);

            //Weighted average between the two results, based on the distance
            float blendedHeight = blendFactor * blendFunctionValue + (1 - blendFactor) * functionValue;
            
            
            Debug.LogFormat("DIstance: {0}. Blend Factor: {1}. Blend func val: {2}. Func val: {3}. Blended Height: {4}",
                distance,
                blendFactor,
                blendFunctionValue,
                functionValue,
                blendedHeight);          
    

            vertices.Add(new Vector3()
            {
                x = point.Position.x,
                y = blendedHeight,
                z = point.Position.y
            });
        });

        mMeshFilter.mesh.vertices = vertices.ToArray();
        mMeshFilter.mesh.RecalculateNormals();
    }

    private Mesh BuildMesh(I2DFunction function, Grid grid, Vector2 sampleOffset)
    {
        List<Vector3> vertices = new List<Vector3>();
        Dictionary<Vector2Int, int> pointToIndexMap = new Dictionary<Vector2Int, int>();

        grid.IterateOverPoints((point) =>
        {
            float height = function.Sample(point.Position + sampleOffset);

            pointToIndexMap[point.GridIndex] = vertices.Count;

            vertices.Add(new Vector3()
            {
                x = point.Position.x,
                y = height,
                z = point.Position.y
            });
        });

        List<int> trianges = new List<int>();

        grid.IterateOverPoints((point) =>
        {
            //Calcuate the indices of the points we want to connect with triangles
            //That is, the point one right, one above and one right and above
            Vector2Int index = point.GridIndex;
            Vector2Int northIndex = index + new Vector2Int(-1, 1);
            Vector2Int eastIndex = index + new Vector2Int(1, 0);
            Vector2Int northEastIndex = index + new Vector2Int(0, 1);

            //If all these points are valid, we add two triangles to the list
            if (pointToIndexMap.ContainsKey(northIndex)
            && pointToIndexMap.ContainsKey(northEastIndex))
            {
                trianges.Add(pointToIndexMap[index]);
                trianges.Add(pointToIndexMap[northIndex]);
                trianges.Add(pointToIndexMap[northEastIndex]);
            }
            
            if (pointToIndexMap.ContainsKey(northEastIndex)
            && pointToIndexMap.ContainsKey(eastIndex))
            {
                trianges.Add(pointToIndexMap[index]);
                trianges.Add(pointToIndexMap[northEastIndex]);
                trianges.Add(pointToIndexMap[eastIndex]);
            }
        });

        Mesh mesh = new Mesh()
        {
            vertices = vertices.ToArray(),
            triangles = trianges.ToArray()
        };

        mesh.RecalculateNormals();
        return mesh;
    }
}
