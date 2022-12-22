using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class PlayerControls : MonoBehaviour
{
    private Camera mainCamera;
    private NavMeshAgent agent;
    public GameObject ComputerPanel;
    public GameObject CrafterPanel;

    bool MovementMode = true;
    bool computerInteractionMode = false;
    bool crafterInteractionMode = false;

    private void OnEnable()
    {
        EventBus.OnGoing += takeInfoPlayer;
    }

    private void OnDisable()
    {
        EventBus.OnGoing -= takeInfoPlayer;
    }

    public void takeInfoPlayer(bool MovementModeInfo)
    {
        MovementMode = MovementModeInfo;
    }

    void Start()
    {
        mainCamera = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        ComputerPanel.SetActive(false);
        CrafterPanel.SetActive(false);
    }
    void Update()
    {
        Movement();
        InteracrionWith();
    }


    public void Movement()
    {
        if (Input.GetMouseButtonDown(0) && MovementMode)
        {
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
    }

    public void InteracrionWith()
    {
        if (Input.GetKeyDown(KeyCode.E) && computerInteractionMode)
        {
            MovementMode = !MovementMode;
            ComputerPanel.SetActive(!ComputerPanel.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.E) && crafterInteractionMode)
        {
            MovementMode = !MovementMode;
            CrafterPanel.SetActive(!CrafterPanel.activeSelf);
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        if (this.CompareTag("Player") && other.CompareTag("ComputerTrigger"))
        {
            computerInteractionMode = true;
        }

        if (this.CompareTag("Player") && other.CompareTag("CrafterTrigger"))
        {
            crafterInteractionMode = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        computerInteractionMode = false;
        crafterInteractionMode = false;
    }

}
