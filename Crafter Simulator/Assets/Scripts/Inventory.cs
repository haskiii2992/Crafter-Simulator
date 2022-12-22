using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [HideInInspector]
    public List<Item> item;

    public GameObject CellContainer;

    bool MovementMode = true;

    public Text Wallet;
    int money = 100;

    void Start()
    {
        item = new List<Item>();

        CellContainer.SetActive(false);

        for (int i = 0; i < CellContainer.transform.childCount; i++)
        {
            CellContainer.transform.GetChild(i).GetComponent<CurrentItems>().index = i;
        }
    }

    void Update()
    {
        ToggleInventory();

        ShowWallet();
    }

    void ShowWallet()
    {
        Wallet.text = Convert.ToString($"{money}$");
    }

    public void BuyItem(int cost)
    {
        money -= cost;
    }

    void ToggleInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            DisplayItems();
            CellContainer.SetActive(!CellContainer.activeSelf);
            MovementMode = !MovementMode;
            EventBus.OnGoing?.Invoke(MovementMode);
        }
    }

    public void TakeItem(Item receivedItem)
    {
        if (receivedItem.isStackable == true && item.Count > 0)
        {
            TakeStackableItem(receivedItem);
        }
        else
        {
            TakeUnstackableItem(receivedItem);
        }
    }

    public void DisplayItems()
    {
        for (int i = item.Count -1; i >= 0; i--)
        {
            Transform cell = CellContainer.transform.GetChild(i);
            Transform icon = cell.GetChild(0);
            Transform count = icon.GetChild(0);

            Text txt = count.GetComponent<Text>();
            Image img = icon.GetComponent<Image>();


            if (item[i].countItem > 1)
            {
               
                txt.text = item[i].countItem.ToString();
                img.enabled = true;
                img.sprite = Resources.Load<Sprite>(item[i].pathIcon);
            }
            else if (item[i].countItem == 1)
            {
                txt.text = null;
                img.enabled = true;
                img.sprite = Resources.Load<Sprite>(item[i].pathIcon);
            }

            else if (item[i].countItem <= 0)
            {
                txt.text = null;
                img.enabled = false;
                item.RemoveAt(i);
                HideItemsInventory();
                DisplayItems();
            }
        }
    }

    void HideItemsInventory()
    {
        for (int i = 0; i < CellContainer.transform.childCount; i++)
        {
            Transform cell = CellContainer.transform.GetChild(i);
            Transform icon = cell.GetChild(0);
            Transform count = icon.GetChild(0);

            Text txt = count.GetComponent<Text>();
            Image img = icon.GetComponent<Image>();

            txt.text = null;
            img.enabled = false;
        }
    }

    void TakeStackableItem(Item receivedItem)
    {
        for (int i = 0; i < item.Count; i++)
        {
            if (item[i].id == receivedItem.id)
            {
                item[i].countItem++;
                DisplayItems();
                return;
            }
        }
        TakeUnstackableItem(receivedItem);
    }

    void TakeUnstackableItem(Item receivedItem)
    {
        if (item.Count < CellContainer.transform.childCount)
        {
            item.Add(new Item());
            item[item.Count - 1] = receivedItem;
            item[item.Count - 1].countItem = 1;

            DisplayItems();
        }
    }

    private void DeleteItemToCraft(int materialItemId, int amount)
    {
        for (int i = 0; i < item.Count; i++)
        {
            if (materialItemId == item[i].id)
            {
                item[i].countItem -= amount;
                DisplayItems();
            }
        }
        DisplayItems();
    }

    public void CraftSword()
    {
        DeleteItemToCraft(1, 1);
        DeleteItemToCraft(2, 2);
    }
}
