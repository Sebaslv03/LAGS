using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{
    public float health;

    private Coroutine healingCoroutine; // Variable para almacenar la referencia de la corutina

    void Update()
    {
        // Puedes agregar lógica adicional aquí si es necesario.
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Si ya hay una corutina en ejecución, no comenzamos una nueva.
            if (healingCoroutine == null)
            {
                healingCoroutine = StartCoroutine(HealPlayer(collision)); // Iniciamos la corutina y la almacenamos.
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (healingCoroutine != null)
            {
                StopCoroutine(healingCoroutine); // Detenemos la corutina usando la referencia almacenada.
                healingCoroutine = null; // Limpiamos la referencia de la corutina.
                collision.GetComponent<Player>().StopHealing(); // Llamamos a StopHealing en el jugador.
            }
        }
    }

    IEnumerator HealPlayer(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            while (true)
            {
                player.Heal(health * Time.deltaTime);
                yield return new WaitForSeconds(Time.deltaTime); // Se ejecuta continuamente mientras esté en el trigger.
            }
        }
    }
}
