using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    [HideInInspector]
    public int countItem;
    public bool isStackable;
    public int id;
    [Multiline(5)]
    public string description;
    public string pathIcon;
    public string pathPrefab;
}
