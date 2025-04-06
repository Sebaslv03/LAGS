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
    public GameObject controller;
    private bool insertPhase = false;
    private void Start()
    {
        slot = GameObject.FindGameObjectWithTag("BatterySlot");
    }
    public override void Interact()
    {
        if (slot.GetComponent<BatterySlot>().isOn)
        {
            Debug.Log("aqui");
            if (fase == 0)
            {
                Debug.Log("fase 1");
                insertPhase = true;
                
                anim.SetBool("Fase1", true);
                fase = 1;
            }
            else if (fase == 1)
            {
                insertPhase = true;   
                anim.SetBool("Fase2", true);
                fase = 2;
            }
            else if (fase == 2)
            {
                insertPhase = true;
                
                anim.SetBool("GateOpen", true);
                fase = 3;
                boxCollider2D.enabled = false;
            }
            slot.GetComponent<BatterySlot>().BatteryLost();
        }
        else
        {
            if(insertPhase)
            {
                if(fase == 1)
                    controller.GetComponent<Controller>().Message("Solo dos más y listo. Espero funcione", 3f);
                else if(fase == 2)
                    controller.GetComponent<Controller>().Message("Corre Corre una más!!!", 3f);
                else if(fase == 3)
                    controller.GetComponent<Controller>().Message("Vamonos de este horrible lugar!!!", 3f);
                insertPhase = false;
            }
            else
                controller.GetComponent<Controller>().Message("Puede que haya baterías por algún lugar voy a buscar", 3f);
        }

    }

    void Update()
    {

    }
}
