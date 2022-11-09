using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public int _wood;
    public int _rock;
    public int _gold;

    [SerializeField] private Text _woodText;
    [SerializeField] private Text _rockText;
    [SerializeField] private Text _goldText;
    [SerializeField] private GameObject notEnoughResourcesPanel;

    private GameObject tileMap;
    private Transform shopPanel;

    private void Start()
    {
        tileMap = GameObject.FindGameObjectWithTag("TileMap");
        shopPanel = gameObject.transform.GetChild(0);
    }

    private void Update()
    {
        _woodText.text = _wood.ToString();
        _rockText.text = _rock.ToString();
        _goldText.text = _gold.ToString();
    }

    public void TryBuy(GameObject buildingPrefab, int woodCost, int rockCost, int goldCost)
    {
        if (woodCost <= _wood && rockCost <= _rock && goldCost <= _gold)
        {
            _wood -= woodCost;
            _rock -= rockCost;
            _gold -= goldCost;

            for (int i = 0; i < tileMap.gameObject.transform.childCount; i++)
            {
                if (tileMap.transform.GetChild(i).GetComponent<BuildingManager>())
                {
                    if ((tileMap.transform.GetChild(i).GetComponent<BuildingManager>().activeCell == true && tileMap.transform.GetChild(i).GetComponent<BuildingManager>().building == false))
                    {
                        tileMap.transform.GetChild(i).GetComponent<BuildingManager>().setBuild(buildingPrefab);
                    }
                }
            }
        }
        else if (woodCost > _wood || rockCost > _rock || goldCost > _gold) 
        {
            Debug.Log("Недостаточно ресурсов");
            notEnoughResourcesPanel.SetActive(true);
        }
        Cancel();
    }

    public void Cancel()
    {
        shopPanel.gameObject.SetActive(false);
        for (int i = 0; i < tileMap.transform.childCount; i++)
        {
            if (tileMap.transform.GetChild(i).GetComponent<BuildingManager>())
            {
                if (tileMap.transform.GetChild(i).GetComponent<BuildingManager>().activeCell == true)
                {
                    tileMap.transform.GetChild(i).GetComponent<BuildingManager>().activeCell = false;
                    break;
                }
            }
        }
    }
}
