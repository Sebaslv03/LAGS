using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public TextMeshProUGUI sanity;
    public TextMeshProUGUI heartbeat;
    public TextMeshProUGUI chat;
    private Player player;
    private Coroutine messageRoutine;
    public TextMeshProUGUI finalText;
    public GameObject canvas;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (player == null)
        {
            Debug.LogError("No se encontro el jugador");
        }
        if (messageRoutine != null)
        {
            StopCoroutine(messageRoutine);
        }
        messageRoutine = StartCoroutine(ShowMessageForTime("¿Dónde estoy? ¿Qué es este portón? Parece que le falta electricidad, voy a investigar...", 6f));
    }

    public void ShowMessages(List<(string text, float duration)> messages)
    {
        if (messageRoutine != null)
        {
            StopCoroutine(messageRoutine);
        }
        messageRoutine = StartCoroutine(ShowMessagesSequence(messages));
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

    public void Message(String s, float duration)
    {
        if (messageRoutine != null)
        {
            StopCoroutine(messageRoutine);
        }
        messageRoutine = StartCoroutine(ShowMessageForTime(s, duration));
    } 

    public void ShowFinalSequence(List<(string text, float duration)> messages)
    {
        if (messageRoutine != null)
        {
            StopCoroutine(messageRoutine);
        }
        messageRoutine = StartCoroutine(ShowMessagesSequence(messages));
    }

    private IEnumerator ShowMessagesSequence(List<(string text, float duration)> messages)
    {
        foreach (var msg in messages)
        {
            finalText.text = msg.text;
            yield return new WaitForSeconds(msg.duration);
        }
        finalText.text = "";
        messageRoutine = null;
         yield return new WaitForSeconds(1f);

    // Cargar el menú
        SceneManager.LoadScene("Menu");

    }
    private IEnumerator ShowMessageForTime(string s, float duration)
    {
        chat.text = s;
        yield return new WaitForSeconds(duration);
        chat.text = "";
        messageRoutine = null;
    }

}
