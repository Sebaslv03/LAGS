using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatterySlot : MonoBehaviour
{

    public Sprite batteryOn;
    public Sprite batteryOff;

    public Image battery;

    public bool isOn;
    public void BatteryOnInventory()
    {
        isOn = true;
        battery.sprite = batteryOn;
    }

    public void BatteryLost()
    {
        isOn = false;
        battery.sprite = batteryOff;
    }
}
