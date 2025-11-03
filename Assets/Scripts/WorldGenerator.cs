using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
using System;

public class WorldGenerator : MonoBehaviour
{
    [Header("Игрок и карта")]
    public Transform heroTranform;
    public Tilemap tilemap;

    [Header("Тайлы")]
    public TileBase sand;
    public TileBase grass;
    public TileBase stone;

    [Header("Чанки")]
    public float sandThreshold, stoneThreshold;
    public int chunkWight, chunkHeight;
    public int xChunkRenderingDistance, yChunkRenderingDistance;

    [Header("Мир")]
    public float noiseScale;
    public int seed;

    private Vector2Int playerChunk;
    private Vector2Int? lastPlayerChunk = null;


    private void Start()
    {
        if (seed == 0)
            seed = UnityEngine.Random.Range(Int16.MinValue, Int16.MaxValue);
    }

    private void FixedUpdate()
    {
        playerChunk = GetPlayerChunk(heroTranform.position.x, heroTranform.position.y);

        if (playerChunk != lastPlayerChunk)
        {
            lastPlayerChunk = playerChunk;
            ClearWorld();
            GenerateChunkAroundPlayer();
        }
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

    public void GenerateChunkAroundPlayer()
    {
        float halfX = xChunkRenderingDistance / 2f;
        float halfY = yChunkRenderingDistance / 2f;

        for (int i = Mathf.CeilToInt(-halfX); i < Mathf.CeilToInt(halfX); i++)
        {
            for (int j = Mathf.CeilToInt(-halfY); j < Mathf.CeilToInt(halfY); j++)
            {
                if (ChunkIsEmpty(playerChunk + new Vector2Int(i, j)))
                    GenerateChunk(playerChunk + new Vector2Int(i, j));
            }
        }

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
                int xPos = chunkPos.x * chunkWight + x;
                int yPos = chunkPos.y * chunkWight + y;
                float noiseValue = Mathf.PerlinNoise(xPos * noiseScale + seed, yPos * noiseScale + seed); // получить значение шума в ху с сидом
                Vector3Int tilePos = new Vector3Int(xPos, yPos, 0);

                SetTile(noiseValue, tilePos);
            }
        }
    }

    private void DeleteChunk(Vector2Int chunkPos)
    {
        for (int x = 0; x < chunkWight; x++)
        {
            for (int y = 0; y < chunkHeight; y++)
            {
                float noiseValue = Mathf.PerlinNoise((chunkPos.x * chunkWight + x) * noiseScale + seed, (chunkPos.y * chunkHeight + y) * noiseScale + seed); // получить значение шума в ху с сидом
                Vector3Int tilePos = new Vector3Int(chunkPos.x * chunkWight + x, chunkPos.y * chunkHeight + y, 0);

                tilemap.SetTile(tilePos, null);
            }
        }
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
            worldGenerator.GenerateChunkAroundPlayer();
        }
        if (GUILayout.Button("Отчистить"))
        {
            worldGenerator.ClearWorld();
        }
    }
}
