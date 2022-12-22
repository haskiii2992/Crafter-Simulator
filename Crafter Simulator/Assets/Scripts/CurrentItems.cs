using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CurrentItems : MonoBehaviour, IPointerClickHandler
{
    [HideInInspector]
    public int index;

    GameObject inventoryObj;
    Inventory inventory;

    void Start()
    {
        inventoryObj = GameObject.FindGameObjectWithTag("InventoryManager");
        inventory = inventoryObj.GetComponent<Inventory>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            inventory.item[index].countItem--;
            inventory.DisplayItems();
        }
    }

}
