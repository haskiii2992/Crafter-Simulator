using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubPanelScripts : MonoBehaviour
{
    public void ClosePanel(GameObject closePanel)
    {
        closePanel.SetActive(false);
    }

    public void OpenPanel(GameObject openPanel)
    {
        openPanel.SetActive(true);
    }
}
