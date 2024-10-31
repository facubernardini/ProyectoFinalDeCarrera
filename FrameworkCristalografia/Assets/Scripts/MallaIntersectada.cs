using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class IntersectionAreaVisualizer : MonoBehaviour
{
    public Vector3 planePosition; // Posición del plano en el espacio
    public Vector3 planeNormal = Vector3.up; // Normal del plano (orientación)
    public Material intersectionMaterial; // Material para la malla de intersección (asignar en Inspector)
    private MeshFilter meshFilter;
    private Mesh intersectionMesh;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();

        if (meshFilter == null || meshFilter.mesh == null)
        {
            Debug.LogError("Este GameObject no tiene un MeshFilter con una malla.");
            return;
        }

        // Crear el plano de corte usando la posición y la normal
        Plane cuttingPlane = new Plane(planeNormal, planePosition);

        // Calcula y crea la malla de intersección
        intersectionMesh = CreateIntersectionMesh(meshFilter.mesh, cuttingPlane);
        DrawIntersectionMesh(intersectionMesh);
    }

    private Mesh CreateIntersectionMesh(Mesh mesh, Plane plane)
    {
        List<Vector3> intersectionVertices = GetIntersectionVertices(mesh, plane);
        if (intersectionVertices.Count < 3)
        {
            Debug.LogWarning("No se encontraron suficientes vértices de intersección.");
            return null;
        }

        // Crear la malla a partir de los vértices de intersección
        Mesh intersectionMesh = new Mesh();
        intersectionMesh.vertices = intersectionVertices.ToArray();

        // Generar triángulos (suponiendo que los vértices forman un polígono cerrado)
        List<int> triangles = new List<int>();
        for (int i = 1; i < intersectionVertices.Count - 1; i++)
        {
            triangles.Add(0);
            triangles.Add(i);
            triangles.Add(i + 1);
        }

        intersectionMesh.triangles = triangles.ToArray();
        intersectionMesh.RecalculateNormals();
        intersectionMesh.RecalculateBounds();

        return intersectionMesh;
    }

    private List<Vector3> GetIntersectionVertices(Mesh mesh, Plane plane)
    {
        List<Vector3> intersectionVertices = new List<Vector3>();
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;

        for (int i = 0; i < triangles.Length; i += 3)
        {
            Vector3 v0 = transform.TransformPoint(vertices[triangles[i]]);
            Vector3 v1 = transform.TransformPoint(vertices[triangles[i + 1]]);
            Vector3 v2 = transform.TransformPoint(vertices[triangles[i + 2]]);

            List<Vector3> triangleIntersection = GetTriangleIntersection(v0, v1, v2, plane);

            if (triangleIntersection != null)
            {
                intersectionVertices.AddRange(triangleIntersection);
            }
        }

        return intersectionVertices;
    }

    private List<Vector3> GetTriangleIntersection(Vector3 v0, Vector3 v1, Vector3 v2, Plane plane)
    {
        List<Vector3> points = new List<Vector3>();

        float d0 = plane.GetDistanceToPoint(v0);
        float d1 = plane.GetDistanceToPoint(v1);
        float d2 = plane.GetDistanceToPoint(v2);

        if (d0 * d1 < 0) points.Add(Vector3.Lerp(v0, v1, d0 / (d0 - d1)));
        if (d1 * d2 < 0) points.Add(Vector3.Lerp(v1, v2, d1 / (d1 - d2)));
        if (d2 * d0 < 0) points.Add(Vector3.Lerp(v2, v0, d2 / (d2 - d0)));

        return points.Count == 2 ? points : null;
    }

    private void DrawIntersectionMesh(Mesh intersectionMesh)
    {
        if (intersectionMesh == null)
        {
            Debug.LogWarning("La malla de intersección es nula, no se puede dibujar.");
            return;
        }

        GameObject intersectionObject = new GameObject("IntersectionMesh");
        intersectionObject.transform.position = Vector3.zero;
        intersectionObject.transform.rotation = Quaternion.identity;

        MeshFilter mf = intersectionObject.AddComponent<MeshFilter>();
        mf.mesh = intersectionMesh;

        MeshRenderer mr = intersectionObject.AddComponent<MeshRenderer>();
        mr.material = intersectionMaterial != null ? intersectionMaterial : new Material(Shader.Find("Standard"));
    }
}
