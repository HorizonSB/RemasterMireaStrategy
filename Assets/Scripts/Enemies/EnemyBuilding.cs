using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBuilding : MonoBehaviour
{
    public float _spawnTimer;
    public GameObject[] _enemies;

    private void Start()
    {
        StartCoroutine("Spawn");
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnTimer);
            Instantiate(_enemies[Random.Range(0, _enemies.Length - 1)], transform.position, Quaternion.identity);
        }
    }
}
