using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
	public MeshRenderer MeshRenderer { get; private set; }
	public MeshFilter MeshFilter { get; private set; }
	public Mesh Mesh { get; private set; }

	[SerializeField] private int resolution = 10;
	[SerializeField] private float radius = 1;
	[SerializeField] private bool _useWorldSpace = true;

	private Vector3[] _vertices;
	private MeshRenderer _meshRenderer;

	private void Awake()
	{
		MeshRenderer = GetComponent<MeshRenderer>();
		MeshFilter = GetComponent<MeshFilter>();
		Mesh = MeshFilter.mesh;
	}

	public void GenerateMesh(Vector3[] positions)
	{
		if (Mesh == null || positions == null || positions.Length <= 1)
		{
			Mesh = new Mesh();
			return;
		}

		var verticesLength = resolution * positions.Length;
		if (_vertices == null || _vertices.Length != verticesLength)
		{
			_vertices = new Vector3[verticesLength];

			var indices = GenerateIndices(positions);
			var uvs = GenerateUVs(positions);

			if (verticesLength > Mesh.vertexCount)
			{
				Mesh.vertices = _vertices;
				Mesh.triangles = indices;
				Mesh.uv = uvs;
			}
			else
			{
				Mesh.triangles = indices;
				Mesh.vertices = _vertices;
				Mesh.uv = uvs;
			}
		}

		var currentVertIndex = 0;

		for (int i = 0; i < positions.Length; i++)
		{
			var circle = CalculateCircle(i, positions);
			foreach (var vertex in circle)
				_vertices[currentVertIndex++] = _useWorldSpace ? transform.InverseTransformPoint(vertex) : vertex;
		}

		Mesh.vertices = _vertices;
		Mesh.RecalculateNormals();
		Mesh.RecalculateBounds();

		MeshFilter.mesh = Mesh;
	}

	private Vector2[] GenerateUVs(Vector3[] positions)
	{
		var uvs = new Vector2[positions.Length * resolution];

		for (int segment = 0; segment < positions.Length; segment++)
		{
			for (int side = 0; side < resolution; side++)
			{
				int vertIndex = (segment * resolution + side);
				float u = side / (resolution - 1f);
				float v = segment / (positions.Length - 1f);

				uvs[vertIndex] = new Vector2(u, v);
			}
		}

		return uvs;
	}

	private int[] GenerateIndices(Vector3[] positions)
	{
		// Two triangles and 3 vertices
		var indices = new int[positions.Length * resolution * 2 * 3];

		int currentIndicesIndex = 0;
		for (int segment = 1; segment < positions.Length; segment++)
		{
			for (int side = 0; side < resolution; side++)
			{
				int vertIndex = (segment * resolution + side);
				int prevVertIndex = vertIndex - resolution;

				// Triangle one
				indices[currentIndicesIndex++] = prevVertIndex;
				indices[currentIndicesIndex++] = (side == resolution - 1) ? (vertIndex - (resolution - 1)) : (vertIndex + 1);
				indices[currentIndicesIndex++] = vertIndex;

				// Triangle two
				indices[currentIndicesIndex++] = (side == resolution - 1) ? (prevVertIndex - (resolution - 1)) : (prevVertIndex + 1);
				indices[currentIndicesIndex++] = (side == resolution - 1) ? (vertIndex - (resolution - 1)) : (vertIndex + 1);
				indices[currentIndicesIndex++] = prevVertIndex;
			}
		}

		return indices;
	}

	private Vector3[] CalculateCircle(int index, Vector3[] positions)
	{
		int dirCount = 0;
		Vector3 forward = Vector3.zero;

		// If not first index
		if (index > 0)
		{
			forward += (positions[index] - positions[index - 1]).normalized;
			dirCount++;
		}

		// If not last index
		if (index < positions.Length - 1)
		{
			forward += (positions[index + 1] - positions[index]).normalized;
			dirCount++;
		}

		// Forward is the average of the connecting edges directions
		forward = (forward / dirCount).normalized;
		Vector3 side = Vector3.Cross(forward, forward + new Vector3(.123564f, .34675f, .756892f)).normalized;
		Vector3 up = Vector3.Cross(forward, side).normalized;

		var circle = new Vector3[resolution];
		float angle = 0f;
		float angleStep = (2 * Mathf.PI) / resolution;

		float t = index / (positions.Length - 1f);

		for (int i = 0; i < resolution; i++)
		{
			float x = Mathf.Cos(angle);
			float y = Mathf.Sin(angle);

			circle[i] = positions[index] + side * x * radius + up * y * radius;

			angle += angleStep;
		}

		return circle;
	}

	private void AddMeshCollider(Mesh mesh, GameObject go)
	{
		if (!go.TryGetComponent(out MeshCollider meshCollider))
			meshCollider = go.gameObject.AddComponent<MeshCollider>();

		meshCollider.sharedMesh = mesh;
		meshCollider.convex = true;
	}
}