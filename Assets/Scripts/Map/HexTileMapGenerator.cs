using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class HexTileMapGenerator : MonoBehaviour
{
    [Header("Map attributes")]
    [SerializeField] private GameObject[] hexTilePrefab;
    List<GameObject> cells = new List<GameObject>();
    [SerializeField] int mapWidth = 25;
    [SerializeField] int mapHeight = 12;

    [Tooltip("Radius in which no enemy buildings are spawned")]
    [SerializeField] float radiusOfNonEnemyBuildings;

    [Header("Level Settings")]
    [SerializeField] int levelNumber;

    [Header("Resources cells")]
    public int cellResourcesAmmount;
    [SerializeField] private GameObject[] resourcesCells;

    [Header("Enemy cells")]
    public int enemyCellsAmmount;
    [SerializeField] private GameObject[] enemyCells;

    float tileXOffset = 1.732f;
    float tileZOffset = 1.500f;

    void Start()
    {
        Time.timeScale = 1;
        EventsBus.LevelPassedSuccesfully?.AddListener(LevelUpgrade);
        CreateListOfCells();
        CreateHexTileMap();
        CheckForNormalGeneration();

    }

    void CheckForNormalGeneration()
    {
        GameObject portal = GameObject.FindGameObjectWithTag("Portal");
        GameObject[] enemyBuildings = GameObject.FindGameObjectsWithTag("EnemyBuilding");
        if (portal == null || enemyBuildings.Length == 0)
        {
            Debug.Log("Second generation");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        foreach (GameObject enemyBuilding in enemyBuildings)
        {
            float distanceToEnemyBuilding = Vector3.Distance(portal.transform.position, enemyBuilding.transform.position);
            if (distanceToEnemyBuilding < radiusOfNonEnemyBuildings)
            {
                Debug.Log("Enemy building spawned too close");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    void CreateListOfCells()
    {
        //Ammount of all cells
        int ammountOfGrassCells = (mapHeight + 1) * (mapWidth + 1);

        //Add portal cell
        cells.Add(hexTilePrefab[0]);

        //Add resources cells
        for (int i = 0; i <= cellResourcesAmmount - 1; i++)
        {
            for (int r = 0; r <= resourcesCells.Length; r++)
            {
                if (r < resourcesCells.Length) cells.Add(resourcesCells[r]);
            }
        }

        //Add cells with enemy buildings
        for (int i = 0; i <= enemyCellsAmmount - 1; i++)
        {
            for (int r = 0; r <= enemyCells.Length; r++)
            {
                if (r < enemyCells.Length) cells.Add(enemyCells[r]);
            }
        }

        //Add grass cells
        for (int i = 0; i <= ammountOfGrassCells; i++)
        {
            cells.Add(hexTilePrefab[1]);
        }
    }

    public void CreateHexTileMap()
    {
        for (int x = 0; x <= mapWidth; x++)
        {
            for (int z = 0; z <= mapHeight; z++)
            {
                int random = Random.Range(0, cells.Count);

                GameObject TempGO = Instantiate(cells[random]);
                TempGO.transform.parent = gameObject.transform;
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

    public void LevelUpgrade()
    {
        if(levelNumber % 3 == 0)
        {
            enemyCellsAmmount += 1;
        }
    }
}
