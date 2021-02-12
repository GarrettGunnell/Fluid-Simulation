using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Fluid : MonoBehaviour {
    public int xSize, zSize;

    private Vector3[] vertices;
    private int[] triangles;
    private Mesh mesh;

    private void Awake() {
        vertices = new Vector3[xSize * zSize];
        triangles = new int[xSize * zSize * 6];
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Fluid";

        for (int i = 0, z = 0; z < zSize; ++z) {
            for (int x = 0; x < xSize; ++x, ++i) {
                vertices[i] = new Vector3(x, 0, z);
            }
        }

        mesh.vertices = vertices;


        for (int z = 0, ti = 0, vi = 0; z < zSize - 1; ++z, ++vi) {
            for (int x = 0; x < xSize - 1; ++vi, ti += 6, ++x) {
                triangles[ti] = vi;
                triangles[ti + 1] = triangles[ti + 4] = vi + xSize;
                triangles[ti + 2] = triangles[ti + 3] = vi + 1;
                triangles[ti + 5] = vi + xSize + 1;
            }
        }
        
        mesh.triangles = triangles;
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
