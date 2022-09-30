using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HexTileMapGenerator : MonoBehaviour
{
    [SerializeField]private GameObject[] hexTilePrefab;


    List<GameObject> cells = new List<GameObject>();

    [SerializeField]int mapWidth = 25;
    [SerializeField]int mapHeight = 12;

    [Header("Resources cells")]
    public int cellResourcesAmmount;
    [SerializeField] private GameObject[] resourcesCells;

    [Header("Enemy cells")]

    float tileXOffset = 1.736f;
    float tileZOffset = 1.506f;

    void Start()
    {
        CreateListOfCells();
        CreateHexTileMap();
    }

    void CreateListOfCells()
    {
        //Ammount of all cells
        int ammountOfGrassCells = (mapHeight + 1) * (mapWidth + 1);
        //Add portal cell
        cells.Add(hexTilePrefab[0]);
        //Add resources cells
        for(int i =0; i<= cellResourcesAmmount-1; i++)
        {
            for(int r = 0; r <= resourcesCells.Length; r++)
            {
                if(r<resourcesCells.Length) cells.Add(resourcesCells[r]);
            }
        }
        //Add grass cells
        for(int i =0; i <= ammountOfGrassCells; i++)
        {
            cells.Add(hexTilePrefab[1]);
        }
    }

    void CreateHexTileMap()
    {
        for(int x = 0; x<= mapWidth; x++)
        {
            for(int z = 0; z <= mapHeight; z++)
            {
                int random = Random.Range(0, cells.Count);

                GameObject TempGO = Instantiate(cells[random]);
                cells.RemoveAt(random);

                if (z % 2 == 0)
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
