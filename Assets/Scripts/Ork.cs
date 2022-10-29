using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ork : ParentCharacter
{
    private Transform _portal;
    private NavMeshAgent _agent;
    [SerializeField] private string _targetTag;
    [SerializeField] private float _detectionRadius;

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        _agent = GetComponent<NavMeshAgent>();
        _portal = GameObject.FindGameObjectWithTag("Portal").transform;
        _agent.SetDestination(_portal.position);
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
            if (distanceToTarget < _detectionRadius) _agent.SetDestination(nearestTarget.transform.position);
        }

        if(_targetTag == null) _agent.SetDestination(_portal.position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _detectionRadius);
    }
}
