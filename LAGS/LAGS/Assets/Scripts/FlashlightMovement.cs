using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class FlashlightMovement : MonoBehaviour
{
    public GameObject Player;
    private Rigidbody2D rb;
    public float rotationSpeed = 15f;
    public float lightRange = 5f;
    public float lightAngle = 45f;
    public LayerMask enemyLayer;
    public int rayCount = 10; // Cantidad de rayos en el cono

    // Start is called before the first frame update
    void Start()
    {
        rb = Player.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        DetectEnemies();
    }

    void DetectEnemies()
    {
        Vector2 direction = transform.right; // Dirección de la linterna
        float startAngle = -lightAngle / 2;
        float angleStep = lightAngle / rayCount;

        for (int i = 0; i < rayCount; i++)
        {
            float angle = startAngle + angleStep * i +90f;
            Vector2 rayDirection = Quaternion.Euler(0, 0, angle) * direction;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, lightRange, enemyLayer);

            if (hit.collider != null)
            {
                EnemyPatrolling enemy = hit.collider.gameObject.GetComponent<EnemyPatrolling>();
                enemy.Lighted();
            }

            Debug.DrawRay(transform.position, rayDirection * lightRange, Color.yellow); // Para depuración
        }
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
