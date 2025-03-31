using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Behaviour{
    Chasing,
    Patrolling,
    RunningFromLight
}
public class Enemy : MonoBehaviour
{
    public Transform target;
    public string enemyName;
    public int hit;
    public float moveSpeed;
    public Rigidbody2D rb;
    public Animator myAnimator;

    public Behaviour state;
    private float runDuration = 1.5f; // Tiempo que huye del jugador
    private float runTimer = 0f;
    public void Lighted()
    {
        state = Behaviour.RunningFromLight;
        runTimer = runDuration;
    }

    public void RunFromLight()
    {
        if (runTimer > 0)
        {
            runTimer -= Time.deltaTime;
            Vector2 fleeDirection = (transform.position - target.position).normalized;
            rb.MovePosition(rb.position + fleeDirection * moveSpeed * Time.fixedDeltaTime); // Se mueve más rápido
        }
        else
        {
            state = Behaviour.Patrolling;
        }
    }
}
