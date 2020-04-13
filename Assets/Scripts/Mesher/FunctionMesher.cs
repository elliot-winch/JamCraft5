using System.Collections.Generic;
using UnityEngine;

public class FunctionMesher
{
    private Grid mGrid;
    private Mesh mMesh;

    public FunctionMesher(Grid grid)
    {
        mGrid = grid;

        mMesh = BuildMesh(grid);
    }

    public Mesh ApplyHeightFunction(I2DFunction function, Vector2 sampleOffset)
    {
        UnityEngine.Profiling.Profiler.BeginSample("Apply function");
        UnityEngine.Profiling.Profiler.BeginSample("Loop");
        List<Vector3> vertices = new List<Vector3>();

        mGrid.IterateOverPoints((point) =>
        {
            float height = function.Sample(point.Position + sampleOffset);

            vertices.Add(new Vector3()
            {
                x = point.Position.x,
                y = height,
                z = point.Position.y
            });
        });

        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Apply result to mesh");
        mMesh.vertices = vertices.ToArray();
        mMesh.RecalculateNormals();
        UnityEngine.Profiling.Profiler.EndSample();
        UnityEngine.Profiling.Profiler.EndSample();
        return mMesh;
    }

    private Mesh BuildMesh(Grid grid)
    {
        UnityEngine.Profiling.Profiler.BeginSample("Create Data");
        UnityEngine.Profiling.Profiler.BeginSample("Verts + UVs");
        List<Vector3> vertices = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        Dictionary<Vector2Int, int> pointToIndexMap = new Dictionary<Vector2Int, int>();

        grid.IterateOverPoints((point) =>
        {
            pointToIndexMap[point.GridIndex] = vertices.Count;

            vertices.Add(new Vector3()
            {
                x = point.Position.x,
                y = 0f,
                z = point.Position.y
            });

            uvs.Add(new Vector2()
            {
                x = point.GridIndex.x / (float)grid.Width,
                y = point.GridIndex.y / (float)grid.Height,
            });
        });
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Triangles");
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
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Create Mesh");
        Mesh mesh = new Mesh()
        {
            vertices = vertices.ToArray(),
            triangles = trianges.ToArray(),
            uv = uvs.ToArray()
        };

        mesh.RecalculateNormals();
        UnityEngine.Profiling.Profiler.EndSample();
        UnityEngine.Profiling.Profiler.EndSample();
        return mesh;
    }
}
