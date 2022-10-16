using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshRebaker : MonoBehaviour
{
    [SerializeField] float timerForBake = 3;
    void Start()
    {
        StartCoroutine(Rebake());
    }

    IEnumerator Rebake()
    {
        yield return new WaitForSeconds(timerForBake);
        GetComponent<NavMeshSurface>().BuildNavMesh();
        StopAllCoroutines();
    }
}
