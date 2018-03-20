using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
[ExecuteInEditMode]
public class VolumeGeometry : MonoBehaviour {

    //Top bottom texture slot
    public Texture VolumeTexture;

    //Reference to the Volume shader
    public Shader VolumeShader;

    //Mesh density dropdown menu
    public enum Density {
        Medium, Low
    }
    public Density _meshDensity = Density.Medium;

    //Drawing style
    public enum DrawingStyle{
        Points
    }
    public DrawingStyle _drawingStyle = DrawingStyle.Points;

    //Private mesh width, height variables
    private int xSize, ySize;


	private void Awake () {
		Generate();
        createMaterial();
	}

    private void calculateDensity () {
        switch(_meshDensity){
            case Density.Medium:
                xSize = 256;
                ySize = 256;
                break;
            case Density.Low:
                xSize = 128;
                ySize = 128;
                break;
            default:
                xSize = 256;
                ySize = 256;
                break;
        }
    }

	private void Generate () {

        Mesh mesh;
        Vector3[] vertices;

        calculateDensity();

        mesh = GetComponent<MeshFilter>().mesh = new Mesh();
        //mesh = 
        mesh.bounds = new Bounds(Vector3.zero, 10000f * Vector3.one);
        mesh.name = "Volume Grid";

		vertices = new Vector3[(xSize + 1) * (ySize + 1)];
		Vector2[] uv = new Vector2[vertices.Length];
		Vector4[] tangents = new Vector4[vertices.Length];
		Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);
		for (int i = 0, y = 0; y <= ySize; y++) {
			for (int x = 0; x <= xSize; x++, i++) {
				vertices[i] = new Vector3(x, y);
				uv[i] = new Vector2((float)x / xSize, (float)y / ySize);
				tangents[i] = tangent;
			}
		}
		mesh.vertices = vertices;
		mesh.uv = uv;
		mesh.tangents = tangents;

		int[] triangles = new int[xSize * ySize * 6];
		for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++) {
			for (int x = 0; x < xSize; x++, ti += 6, vi++) {
				triangles[ti] = vi;
				triangles[ti + 3] = triangles[ti + 2] = vi + 1;
				triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
				triangles[ti + 5] = vi + xSize + 2;
			}
		}
		mesh.triangles = triangles;
		mesh.RecalculateNormals();

        //Set the drawing mesh topolgy
        mesh.SetIndices(mesh.triangles, MeshTopology.Points, 0);

	}

	private void createMaterial(){
        
        Renderer rend = GetComponent<Renderer>();
        rend.sharedMaterial = new Material(VolumeShader);
        rend.sharedMaterial.mainTexture = VolumeTexture;
        rend.sharedMaterial.SetFloat("_Displacement", 220.0f);

    }
}