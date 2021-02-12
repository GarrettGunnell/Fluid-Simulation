using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Fluid : MonoBehaviour {
    public int xSize, zSize;

    private Vector3[] vertices;
    private int[] triangles;
    private Vector2[] uv;
    private Vector4[] tangents;
    private Mesh mesh;

    private void Awake() {
        vertices = new Vector3[xSize * zSize];
        triangles = new int[xSize * zSize * 6];
        uv = new Vector2[xSize * zSize];
        tangents = new Vector4[xSize * zSize];
        Vector4 tangent = new Vector4(0f, 1f, 0f, -1f);

        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Fluid";

        for (int i = 0, z = 0; z < zSize; ++z) {
            for (int x = 0; x < xSize; ++x, ++i) {
                vertices[i] = new Vector3(x, 0, z);
                uv[i] = new Vector2((float)x / xSize, (float)z / zSize);
                tangents[i] = tangent;
            }
        }

        for (int z = 0, ti = 0, vi = 0; z < zSize - 1; ++z, ++vi) {
            for (int x = 0; x < xSize - 1; ++vi, ti += 6, ++x) {
                triangles[ti] = vi;
                triangles[ti + 1] = triangles[ti + 4] = vi + xSize;
                triangles[ti + 2] = triangles[ti + 3] = vi + 1;
                triangles[ti + 5] = vi + xSize + 1;
            }
        }

        mesh.vertices = vertices;        
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.tangents = tangents;
        mesh.RecalculateNormals();
    }

    private void OnDrawGizmos() {
        if (vertices == null) return;
        Gizmos.color = Color.black;
        for (int i = 0; i < vertices.Length; ++i) {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
}
