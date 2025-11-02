using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
using System;

public class WorldGenerator : MonoBehaviour
{
    public Transform heroTranform;
    public Tilemap tilemap;
    public TileBase sand, grass, stone;
    public float sandThreshold, stoneThreshold;

    public int chunkWight = 16, chunkHeight = 16;

    public float noiseScale = 0.1f;
    public int seed = 0;


    private void Start()
    {
        if (seed == 0)
            seed = UnityEngine.Random.Range(Int16.MinValue, Int16.MaxValue);
    }

    private void FixedUpdate()
    {
        GenerateChunkUnderPlayer();
    }

    public void ClearWorld()
    {
        tilemap.ClearAllTiles();
    }

    public void GenerateChunkUnderPlayer()
    {
        var playerChunk = GetPlayerChunk(heroTranform.position.x, heroTranform.position.y);
        Debug.Log(playerChunk);
        if (ChunkIsEmpty(playerChunk)) GenerateChunk(playerChunk);
    }

    private void SetTile(float noiseValue, Vector3Int tilePos)
    {
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

    private void GenerateChunk(Vector2Int chunkPos)
    {
        for (int x = 0; x < chunkWight; x++)
        {
            for (int y = 0; y < chunkHeight; y++)
            {
                float noiseValue = Mathf.PerlinNoise((chunkPos.x * chunkWight + x) * noiseScale + seed, (chunkPos.y * chunkHeight + y) * noiseScale + seed); // получить значение шума в ху с сидом
                Vector3Int tilePos = new Vector3Int(chunkPos.x * chunkWight + x, chunkPos.y * chunkHeight + y, 0);

                SetTile(noiseValue, tilePos);
            }
        }
        Debug.Log($"Chunk - {chunkPos.x}:{chunkPos.y} - generation complete!");
    }

    private Vector2Int GetPlayerChunk(float xPlayer, float yPlayer)
    {
        int x = Mathf.FloorToInt(xPlayer / chunkWight);
        int y = Mathf.FloorToInt(yPlayer / chunkHeight);
        return new Vector2Int(x, y);
    }

    private bool ChunkIsEmpty(Vector2Int chunkPos)
    {
        return tilemap.GetTile(new Vector3Int(chunkPos.x * chunkWight, chunkPos.y * chunkHeight, 0)) is null;
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
            worldGenerator.GenerateChunkUnderPlayer();
        }
        if (GUILayout.Button("Отчистить"))
        {
            worldGenerator.ClearWorld();
        }
    }
}
