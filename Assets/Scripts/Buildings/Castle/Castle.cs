using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    public int spawnSoldiersAmmount;
    public float timeTillNextSpawn = 5;

    [Header("Unity Setup Fields")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform[] wanderPoints;
    [SerializeField] private GameObject soldierPrefab;
    [SerializeField] private int aliveSoldiers = 0;

    public void Start()
    {
        StartCoroutine("SpawnSoldiers");
    }

    IEnumerator SpawnSoldiers()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeTillNextSpawn);
            if (aliveSoldiers < spawnSoldiersAmmount)
            {
                GameObject soldier = Instantiate(soldierPrefab, spawnPoint.position, Quaternion.identity);
                soldier.GetComponent<Soldier>().StartWandering(wanderPoints);
                aliveSoldiers += 1;
                soldier.GetComponent<Soldier>().death.AddListener(SoldierDie);
            }
        }
    }

    public void SoldierDie() => aliveSoldiers -= 1;
}
