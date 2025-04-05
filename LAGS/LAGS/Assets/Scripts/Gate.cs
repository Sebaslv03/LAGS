using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : Interactable
{
    public Animator anim;
    public BoxCollider2D boxCollider2D;
    private int fase = 0;
    private GameObject slot;
    private void Start()
    {
        slot = GameObject.FindGameObjectWithTag("BatterySlot");
    }
    public override void Interact()
    {
        if (slot.GetComponent<BatterySlot>().isOn)
        {
            Debug.Log("Interactuando con la puerta");
            slot.GetComponent<BatterySlot>().BatteryLost();
            if (fase == 0)
            {
                anim.SetBool("Fase1", true);
                fase = 1;
            }
            else if (fase == 1)
            {
                anim.SetBool("Fase2", true);
                fase = 2;
            }
            else if (fase == 2)
            {
                anim.SetBool("GateOpen", true);
                fase = 3;
                boxCollider2D.enabled = false;
            }
        }

    }

    void Update()
    {

    }
}
