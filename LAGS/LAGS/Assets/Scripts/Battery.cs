using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : Interactable
{
    private GameObject slot;
    public GameObject controller;

    private void Start()
    {
        slot = GameObject.FindGameObjectWithTag("BatterySlot");
        
    }
    public override void Interact()
    {
        if (!slot.GetComponent<BatterySlot>().isOn)
        {
            gameObject.SetActive(false);
            slot.GetComponent<BatterySlot>().BatteryOnInventory();
            controller.GetComponent<Controller>().Message("Que bien voy a ir a ponerla", 3f);
        }else{
            controller.GetComponent<Controller>().Message("Primero tengo que ir a dejar esta que tengo", 3f);
        }
    }


}
