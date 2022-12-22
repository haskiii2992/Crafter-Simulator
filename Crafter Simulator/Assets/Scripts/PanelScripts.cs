using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelScripts : MonoBehaviour
{
    bool MovementMode = true;

    public void ClosePanel(GameObject closePanel)
    {
        closePanel.SetActive(false);
        MovementMode = true;
        EventBus.OnGoing?.Invoke(MovementMode);
    }

    public void OpenPanel(GameObject openPanel)
    {
        openPanel.SetActive(true);
        MovementMode = false;
        EventBus.OnGoing?.Invoke(MovementMode);
    }
}
