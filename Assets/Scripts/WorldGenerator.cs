using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

public class WorldGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase sand;
    public TileBase grass;
    public TileBase stone;
    public int width = 16;
    public int height = 16;
    public float noiseScale = 0.1f;
    public float sandThreshold = 0.8f;
    public float stoneThreshold = 0.2f;

    private void Start()
    {
        GenerateWorld();
    }


    public void GenerateWorld()
    {
        tilemap.ClearAllTiles();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float noiseValue = Mathf.PerlinNoise(x * noiseScale, y * noiseScale);
                Debug.Log($"{noiseValue} - {x}:{y}");

                Vector3Int tilePos = new Vector3Int(x, y, 0);

                if (noiseValue > sandThreshold)
                {
                    tilemap.SetTile(tilePos, sand);
                }
                else if (noiseValue < stoneThreshold)
                {
                    tilemap.SetTile(tilePos, stone);
                }
                else
                {
                    tilemap.SetTile(tilePos, grass);
                }
            }
        }
    }
}

[CustomEditor(typeof(WorldGenerator))]
public class WorldGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        WorldGenerator worldGenerator = (WorldGenerator)target;

        if (GUILayout.Button("Перегенерировать"))
        {
            worldGenerator.GenerateWorld();
        }
    }
}
