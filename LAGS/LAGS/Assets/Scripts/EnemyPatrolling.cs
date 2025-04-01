using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPatrolling : Enemy
{
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;

    private Vector2 direction;
    public Transform[] path;
    public int currentPoint;
    public Transform currentGoal;
    bool isAttacking;
    public SpriteRenderer spriteRenderer;


    void Start()
    {
        state = Behaviour.Patrolling;
        target = GameObject.FindWithTag("Player").transform;
        if (path != null && path.Length != 0)
        {
            currentGoal = path[currentPoint];
        }
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CheckPlayerHealing())
        {
            state = Behaviour.Patrolling;
        }
        else if (state == Behaviour.Patrolling && CheckDistance(chaseRadius))
        {
            state = Behaviour.Chasing;
        }
        else if (state == Behaviour.Chasing && !CheckDistance(chaseRadius))
        {
            state = Behaviour.Patrolling;
        }
        if (CheckDistance(attackRadius))
        {
            if (!isAttacking)
            {
                StartCoroutine(DamagePlayer());
            }
        }
        if (state == Behaviour.Chasing)
        {
            ChasePlayer();
        }
        else if (state == Behaviour.Patrolling)
        {
            Patrol();
        }
        else if (state == Behaviour.RunningFromLight)
        {
            RunFromLight();
        }
    }
    void Update()
    {
        myAnimator.SetFloat("Speed", direction.magnitude);
        myAnimator.SetFloat("Horizontal", direction.x);
        myAnimator.SetFloat("Vertical", direction.y);
        spriteRenderer.flipX = direction.x < 0;
    }

    bool CheckPlayerHealing()
    {
        return target.GetComponent<Player>().IsHealing();
    }

    void Patrol()
    {
        if (path.Length <= 0)
        {
            return;
        }
        if (Vector3.Distance(rb.position, currentGoal.position) > 0.1)
        {
            direction = (currentGoal.position - this.transform.position).normalized;
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            currentPoint += 1;
            currentPoint %= path.Length;
            currentGoal = path[currentPoint];
        }
    }

    void ChasePlayer()
    {
        // Calculate direction
        direction = (target.position - transform.position).normalized;

        // Move towards the target
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);

    }

    IEnumerator DamagePlayer()
    {
        isAttacking = true;
        while (CheckDistance(attackRadius)) // Mientras el jugador esté cerca
        {
            Player playerScript = target.GetComponent<Player>();
            if (playerScript != null && playerScript.sanity >= 5)
            {
                playerScript.sanity -= hit; // Reducir sanidad
            }
            yield return new WaitForSeconds(2); // Esperar antes de volver a hacer daño
        }
        isAttacking = false;
    }

    bool CheckDistance(float radius)
    {
        return Vector3.Distance(transform.position, target.position) <= radius;
    }
}
