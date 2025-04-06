using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour
{
    public GameObject controller;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        controller.GetComponent<Controller>().canvas.SetActive(false);
        collision.GetComponent<Movement>().canMove = false;
        collision.gameObject.SetActive(false);
        var messages = new List<(string, float)>
        {
            ("Felicidades! Lograste Salir!", 3f),
            ("Pero... Dejaste la puerta abierta", 3f),
            ("Ahora...", 2f),
            ("Ellos...", 2f),
            ("También... saldrán", 2f),
            ("NOS VEMOS PRONTO!", 4f)
        };

        controller.GetComponent<Controller>().ShowFinalSequence(messages);
    }
}
