using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orders : MonoBehaviour
{
    public GameObject content;
    public GameObject prefabOfOrderPanel;

    public int maxAmountOfOrders = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckAmountOfOrders();
    }

    public void CheckAmountOfOrders()
    {
        if (content.transform.childCount < maxAmountOfOrders)
        {
            CreateOrder();
            CheckAmountOfOrders();
        }
    }

    public void CreateOrder()
    {
        var orderPanel = Instantiate(prefabOfOrderPanel);
        orderPanel.transform.SetParent(content.transform, false);
    }
}
