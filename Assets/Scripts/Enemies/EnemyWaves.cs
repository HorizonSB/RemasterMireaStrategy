using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyWaves : MonoBehaviour
{
    List<GameObject> enemiesBuilding = new List<GameObject>();
    [SerializeField]private float gameTimer = 300f;
    [SerializeField] private Text timerText;

    void Start()
    {
        InvokeRepeating("BuildEnemySpawn", 1f, 0.5f);
        GameObject[] enemyBuildings = GameObject.FindGameObjectsWithTag("EnemyBuilding");
        for (int i = 0; i < enemyBuildings.Length; i++)
        {
            enemiesBuilding.Add(enemyBuildings[i]);
        }
    }


    private void Update()
    {
        gameTimer -= Time.deltaTime;
        timerText.text = gameTimer.ToString();
    }

    private void BuildEnemySpawn()
    {
        int count = Random.Range(0, enemiesBuilding.Count);
        if (enemiesBuilding.Count != 0 && Mathf.RoundToInt(gameTimer)%30 == 0)
        {
            enemiesBuilding[count].transform.GetChild(0).gameObject.SetActive(true);
            enemiesBuilding.RemoveAt(count);
        }
    }
}
