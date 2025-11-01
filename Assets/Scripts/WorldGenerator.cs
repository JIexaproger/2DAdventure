using UnityEngine;
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

    void Start()
    {
        GenerateWorld();
    }

    void GenerateWorld()
    {
        tilemap.ClearAllTiles();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float noiseValue = Mathf.PerlinNoise(x, y);

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
