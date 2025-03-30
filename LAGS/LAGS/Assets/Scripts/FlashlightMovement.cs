using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class FlashlightMovement : MonoBehaviour
{
    public GameObject Player;
    private Rigidbody2D rb;
    public float rotationSpeed = 15f;

    // Start is called before the first frame update
    void Start()
    {
        rb = Player.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (rb.velocity != Vector2.zero)
        {
            // Calcular la dirección del jugador al objeto (sin considerar el eje y)
            Vector2 direction = rb.velocity.normalized;

            // Calcular el ángulo en el plano 2D
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

            // Interpolación suave entre la rotación actual y la nueva
            float smoothAngle = Mathf.LerpAngle(transform.eulerAngles.z, angle, rotationSpeed * Time.fixedDeltaTime);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, smoothAngle));
        }
    }
}
