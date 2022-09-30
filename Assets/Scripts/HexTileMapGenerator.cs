using UnityEngine;

public class HexTileMapGenerator : MonoBehaviour
{
    [SerializeField]private GameObject hexTilePrefab;

    [SerializeField]int mapWidth = 25;
    [SerializeField]int mapHeight = 12;

    float tileXOffset = 1.736f;
    float tileZOffset = 1.506f;

    void Start()
    {
        CreateHexTileMap();
    }

    void CreateHexTileMap()
    {
        for(int x = 0; x<= mapWidth; x++)
        {
            for(int z = 0; z <= mapHeight; z++)
            {
                GameObject TempGO = Instantiate(hexTilePrefab);

                if(z % 2 == 0)
                {
                    TempGO.transform.position = new Vector3(x * tileXOffset, 0, z * tileZOffset);
                }
                else
                {
                    TempGO.transform.position = new Vector3(x * tileXOffset + tileXOffset / 2, 0, z * tileZOffset);
                }
            }
        }
    }
}
