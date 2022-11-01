using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBuilding : MonoBehaviour
{
    [SerializeField] private float _gameDuration;
    [SerializeField] private AnimationCurve timeSpawnCurve; 
    public GameObject[] _enemies;

    private void Start()
    {
        StartCoroutine("Spawn");
    }

    private void Update()
    {
        _gameDuration += Time.deltaTime;
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeSpawnCurve.Evaluate(_gameDuration));
            Instantiate(_enemies[Random.Range(0, _enemies.Length - 1)], transform.position, Quaternion.identity);
        }
    }
}
