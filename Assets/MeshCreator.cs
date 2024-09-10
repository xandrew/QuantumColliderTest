using UnityEngine;
using UnityEditor; // Required for saving assets

public class CreateAndSaveSquareMesh : MonoBehaviour
{
    void Start()
    {
        // Create a new mesh
        Mesh mesh = new Mesh();

        // Define the vertices
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(-1, -0.1f, -1),  // Bottom-left
            new Vector3(1, 0.1f, -1),  // Bottom-right
            new Vector3(-1, -0.1f, 1),  // Top-left
            new Vector3(1, 0.1f, 1)   // Top-right
        };

        // Define the triangles (2 triangles to form a square)
        int[] triangles = new int[]
        {
            1, 0, 2, // First triangle (bottom-left)
            3, 1, 2  // Second triangle (top-right)
        };

        // Assign the vertices and triangles to the mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        // Optional: Recalculate the normals to get proper lighting
        mesh.RecalculateNormals();

        // Call function to save the mesh as an asset
        SaveMeshAsAsset(mesh, "Assets/SavedSquareMesh.asset");
    }

    void SaveMeshAsAsset(Mesh mesh, string assetPath)
    {
        // Check if the editor is running (since AssetDatabase is editor-only)
#if UNITY_EDITOR
        // Ensure the mesh has a name
        mesh.name = "SquareMesh";

        // Save the mesh asset to the specified path
        AssetDatabase.CreateAsset(mesh, assetPath);

        // Save the changes to the asset database
        AssetDatabase.SaveAssets();

        // Log a message to confirm
        Debug.Log("Mesh saved as an asset at: " + assetPath);
#endif
    }
}
