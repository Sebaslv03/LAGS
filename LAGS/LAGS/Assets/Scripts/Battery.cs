using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : Interactable
{
    private GameObject slot;

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

        }
    }


}
