using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolling : Enemy
{
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;
    
    private Vector2 direction;
    public Transform[] path;
    public int currentPoint;
    public Transform currentGoal;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        currentGoal = path[currentPoint];
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CheckDistance())
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }
    void Update()
    {
        myAnimator.SetFloat("Speed", direction.magnitude);
        myAnimator.SetFloat("Horizontal",direction.x);
        myAnimator.SetFloat("Vertical", direction.y);
        spriteRenderer.flipX = direction.x < 0;
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
    bool CheckDistance()
    {
        return Vector3.Distance(this.transform.position, target.position) <= chaseRadius;
    }
}
