using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float sanity = 100f;
    public float heartbeat = 90;
    private float sanityDecrement = 1f;
    private bool isHealing = false;
    private bool gameOverStarted = false;
    AudioManager audioManager;
    public GameObject camara;
    public Animator anim;

    void Start()
    {
        audioManager = GameObject.FindWithTag("Audio").GetComponent<AudioManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckInteraction();
        }

        if (sanity > 0 && !isHealing)
        {
            sanity -= sanityDecrement * Time.deltaTime;
        }

        // Iniciar cuenta regresiva de Game Over si la sanidad llegó a cero
        if (sanity <= 0 && !gameOverStarted)
        {
            StartCoroutine(GameOverCountdown());
        }

        float sanityPercentage = Mathf.Clamp01(sanity / 100f);
        float heartbeatSpeed = 1f - sanityPercentage;
        heartbeat = Mathf.Lerp(90f, 200f, heartbeatSpeed);
        audioManager.ChangeHeartBeat(heartbeatSpeed);
    }

    private IEnumerator GameOverCountdown()
{
    gameOverStarted = true;
    Debug.Log("Sanidad en 0. El juego terminará en 3 segundos...");
    yield return new WaitForSeconds(3f);
    yield return StartCoroutine(GameOver()); // Esperar la animación
    SceneManager.LoadScene("Menu");
}

private IEnumerator GameOver()
{
    // Activar animación
    anim.SetBool("Death", true);

    // Zoom
    yield return StartCoroutine(ZoomInOnDeath());

    // Esperar a que termine la animación
    AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
    float animLength = stateInfo.length;

    yield return new WaitForSeconds(2f);
}

    private IEnumerator ZoomInOnDeath()
{
    var cam = camara.GetComponent<CinemachineVirtualCamera>();
    var noise = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    noise.m_AmplitudeGain = 0;

    float startSize = cam.m_Lens.OrthographicSize;
    float endSize = 0.3f; // Más pequeño = más zoom
    float duration = 1.5f;
    float t = 0;

    while (t < duration)
    {
        cam.m_Lens.OrthographicSize = Mathf.Lerp(startSize, endSize, t / duration);
        t += Time.deltaTime;
        yield return null;
    }

    cam.m_Lens.OrthographicSize = endSize;
}

    public void Heal(float healAmount)
    {
        isHealing = true;
        sanity += healAmount;
        if (sanity > 100f)
        {
            sanity = 100f;
        }
    }

    private void CheckInteraction()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, new Vector2(0.1f, 1f), 0, Vector2.zero);
        foreach (RaycastHit2D rc in hits)
        {
            var interactable = rc.transform.GetComponent<Interactable>();
            if (interactable)
            {
                interactable.Interact();
            }
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
