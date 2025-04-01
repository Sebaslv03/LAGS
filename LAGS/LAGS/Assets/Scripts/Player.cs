using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float sanity = 100f;
    public float heartbeat = 90;
    private float sanityDecrement = 0.5f;
    private bool isHealing = false;
    AudioManager audioManager;

    void Start()
    {
        audioManager = GameObject.FindWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sanity >= 0 && !isHealing)
        {
            // Reducir la sanidad del jugador
            sanity -= sanityDecrement * Time.deltaTime;
        }

        // Calcular el porcentaje de sanidad (de 0 a 1)
        float sanityPercentage = Mathf.Clamp01(sanity / 100f); // Normaliza la sanidad a un rango entre 0 y 1

        // El ritmo cardíaco aumenta cuando la sanidad disminuye
        // Cuando la sanidad es 100, el latido está normal (1)
        // Cuando la sanidad es 0, el latido está acelerado (2)
        float heartbeatSpeed = 1f - sanityPercentage; // Invertir el porcentaje para hacer que el latido aumente cuando la sanidad baja

        heartbeat = Mathf.Lerp(90f, 200f, heartbeatSpeed);
        // Cambiar el ritmo del corazón
        audioManager.ChangeHeartBeat(heartbeatSpeed);
    }

    public void Heal(float healAmount)
    {
        isHealing = true;
        // Aumentar la sanidad del jugador
        sanity += healAmount;
        // Limitar la sanidad a un máximo de 100
        if (sanity > 100f)
        {
            sanity = 100f;
        }

    }
    public void StopHealing()
    {
        isHealing = false;
    }

    public bool IsHealing()
    {
        return isHealing;
    }



}
