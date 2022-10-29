using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShoot : MonoBehaviour
{
    [SerializeField] private string _targetTag;
    [SerializeField] private GameObject projectilePrefab;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(_targetTag);
        float shortetsDistance = Mathf.Infinity;
        GameObject nearestTarget = null;
        foreach (GameObject target in targets)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            if (distanceToTarget < shortetsDistance)
            {
                shortetsDistance = distanceToTarget;
                nearestTarget = target;
            }
        }
        if (_targetTag == null) StopAllCoroutines();
        if (nearestTarget)
        {
            Instantiate(projectilePrefab, gameObject.transform.position, Quaternion.identity);
        }
    }
}
