using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    public GameObject buildingPrefab;

    public int _woodCost;
    public int _rockCost;
    public int _goldCost;

    [SerializeField] private Text _woodText;
    [SerializeField] private Text _rockText;
    [SerializeField] private Text _goldText;

    [SerializeField] private GameObject shopPanel;

    private void Start()
    {
        shopPanel = GameObject.FindGameObjectWithTag("ShopPanel");
        _woodText.text = "Дерево: " + _woodCost.ToString();
        _rockText.text = "Камень: " + _rockCost.ToString();
        _goldText.text = "Золото: " + _goldCost.ToString();
    }

    public void BuyBuilding()
    {
        shopPanel.GetComponent<Shop>().TryBuy(buildingPrefab, _woodCost, _rockCost, _goldCost);
    }
}
