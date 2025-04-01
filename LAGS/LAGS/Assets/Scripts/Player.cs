using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float sanity = 100f;
    public int heartbeat = 90;
    private float sanityDecrement = 0.5f;
    AudioManager audioManager;

    void Start()
    {
        audioManager = GameObject.FindWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sanity >= 0)
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

        // Cambiar el ritmo del corazón
        audioManager.ChangeHeartBeat(heartbeatSpeed);
    }

    

}
