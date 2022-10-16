using UnityEngine;
using UnityEngine.EventSystems;


public class BuildingManager : MonoBehaviour, IPointerClickHandler
{

    public bool building;
    public bool activeCell;
    [SerializeField]private Transform shopPanel;

    void Start()
    {
        shopPanel = GameObject.Find("Shop").transform.Find("ShopPanel");
    }

    void Update()
    {
        if (shopPanel.gameObject.activeInHierarchy) return;
        if (activeCell) gameObject.SetActive(false);
    }

    public void setBuild(GameObject build)
    {
        Instantiate(build).transform.position = transform.GetChild(0).transform.position;
        building = true;
        activeCell = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(name + " Game object clicked");
        if (!building)
        {
            shopPanel.gameObject.SetActive(true);
            activeCell = true;
        }
    }
}
