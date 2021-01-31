using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private MeshFilter _meshFilter;

    [SerializeField] private int _rayCount = 36;
    [Range(0, 360)] [SerializeField] private float _fovAngle = 90f;
    [SerializeField] private float _range = 5f;
    [SerializeField] private LayerMask _layerMask;

    private Mesh _mesh;

    private Vector3 _origin;
    private float _angle;

    private void Start()
    {
        _mesh = new Mesh();
        _meshFilter.mesh = _mesh;
    }

    private void Update()
    {
        SetOrigin(Vector3.zero);
        GenerateMesh();
    }

    private void GenerateMesh()
    {
        float fovAngleRad = (Mathf.PI / 180f) * _fovAngle;
        float angleStep = fovAngleRad / _rayCount;

        Vector3[] vertices = new Vector3[_rayCount + 2];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[_rayCount * 3 + 3];

        vertices[0] = _origin;
        uv[0] = Vector2.zero;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= _rayCount; i++)
        {
            float vertexAngle = _angle + (fovAngleRad * 0.5f) - angleStep * i;
            Vector3 rayDirection = new Vector3(Mathf.Cos(vertexAngle), Mathf.Sin(vertexAngle));
            Vector3 vertex = _origin + rayDirection * _range;

            RaycastHit2D collision = Physics2D.Raycast(transform.position, rayDirection, _range, _layerMask);
            if (collision.collider != null)
            {
                vertex = new Vector3(collision.point.x, collision.point.y) - transform.position;
            }

            vertices[vertexIndex] = vertex;
            uv[vertexIndex] = new Vector2(vertex.x, vertex.y);

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;
            }

            triangleIndex += 3;
            vertexIndex++;
        }

        _mesh.vertices = vertices;
        _mesh.uv = uv;
        _mesh.triangles = triangles;
    }

    private void SetOrigin(Vector3 origin)
    {
        _origin = origin;
    }

    public void SetDirection(Vector3 direction)
    {
        direction = direction.normalized;
        _angle = Mathf.Atan2(direction.y, direction.x);
        if (_angle < 0)
        {
            _angle += Mathf.PI * 2;
        }
    }

}
