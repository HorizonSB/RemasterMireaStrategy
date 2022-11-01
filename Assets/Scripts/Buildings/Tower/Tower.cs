using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private string _targetTag;

    [Header("Attributes")]
    public float range = 2f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Unity Setup Fields")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    [SerializeField]private Transform _target;

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
        if(nearestTarget != null && shortetsDistance <= range)
        {
            _target = nearestTarget.transform;
        }
        else
        {
            _target = null;
        }
    }

    private void Update()
    {
        if (_target == null) return;

        if(fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject projectileTempGO = Instantiate(projectilePrefab, firePoint.position,Quaternion.identity);
        Arrow arrow = projectileTempGO.GetComponent<Arrow>();

        if (arrow != null) arrow.Seek(_target);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
