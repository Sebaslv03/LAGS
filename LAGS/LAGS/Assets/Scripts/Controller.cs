using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public TextMeshProUGUI sanity;
    public TextMeshProUGUI heartbeat;
    private Player player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (player == null)
        {
            Debug.LogError("No se encontro el jugador");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (sanity == null)
        {
            Debug.LogError("No se encontro el texto");
            return;
        }
        sanity.text = ((int) player.sanity).ToString();
        heartbeat.text = ((int) player.heartbeat).ToString();
    }
}
