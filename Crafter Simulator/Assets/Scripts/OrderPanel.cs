using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderPanel : MonoBehaviour
{
    public int countOfPredmets;
    public int coastOfOne = 50;
    public int coast;
    public string pathIcon;
    // Start is called before the first frame update
    void Start()
    {
        countOfPredmets = Random.Range(1, 3);
        coast = coastOfOne * countOfPredmets; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DrawPanel()
    {
        Transform nameOfProductText = transform.GetChild(2);
        Transform costText = transform.GetChild(3);
     //   Image Icon
    }
}
