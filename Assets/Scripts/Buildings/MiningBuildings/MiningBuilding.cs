using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningBuilding : MonoBehaviour
{
    [Header("Attributes")]
    public float miningRange;
    public int ammountOfExtractedResource;
    public float productionRate;

    [Header("Unity Setup Fields")]
    public string resourceTag;


    private Shop shop;

    private void Start()
    {
        shop = GameObject.FindGameObjectWithTag("ShopPanel").GetComponent<Shop>();

        GameObject[] resources = GameObject.FindGameObjectsWithTag(resourceTag);
        foreach(GameObject resource in resources)
        {
            float distance = Vector3.Distance(transform.position, resource.transform.position);
            if(distance <= miningRange)
            {
                StartCoroutine(Mining());
            }
        }
    }

    IEnumerator Mining()
    {
        while (true)
        {
            yield return new WaitForSeconds(productionRate);
            switch (resourceTag)
            {
                case "Forest":
                    shop._wood += ammountOfExtractedResource;
                    break;
                case "Rock":
                    shop._rock += ammountOfExtractedResource;
                    break;
                case "Gold":
                    shop._gold += ammountOfExtractedResource;
                    break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, miningRange);
    }
}
