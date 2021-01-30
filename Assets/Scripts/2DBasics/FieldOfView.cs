using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private MeshFilter _meshFilter;

    private Vector2 _direction;
    private Mesh _mesh;

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    private void GenerateMesh()
    {
        List<Vector3> vertices = new List<Vector3>();
        List<Vector2> uv = new List<Vector2>();
        List<int> triangles = new List<int>();

        vertices.Add(Vector3.zero);
        vertices.Add(new Vector3(-50, 50, 0));
        vertices.Add(new Vector3(50, 50, 0));

        _mesh.vertices = vertices.ToArray();
        _mesh.uv = uv.ToArray();
        _mesh.triangles = triangles.ToArray();
    }

    private void Start()
    {
        _mesh = new Mesh();
        _meshFilter.mesh = _mesh;
    }

    private void FixedUpdate()
    {
        GenerateMesh();
    }

}
