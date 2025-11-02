using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
using System;

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

    [SerializeField]
    public int? seed = null;


    private void Start()
    {
        GenerateWorld();
    }

    public void ClearWorld()
    {
        tilemap.ClearAllTiles();
    }


    public void GenerateWorld()
    {
        tilemap.ClearAllTiles();

        if (seed is null)
        {
            seed = UnityEngine.Random.Range(Int16.MinValue, Int16.MaxValue);
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float noiseValue = Mathf.PerlinNoise((float)(x + seed) * noiseScale, (float)(y + seed) * noiseScale);
                Debug.Log($"{noiseValue} - {x}:{y}");

                Vector3Int tilePos = new Vector3Int(x-(width/2), y-(height/2), 0);

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
        if (GUILayout.Button("Отчистить"))
        {
            worldGenerator.ClearWorld();
        }
    }
}
