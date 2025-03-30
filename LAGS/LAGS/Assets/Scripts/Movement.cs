using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    public Animator animator;
    private Vector2 moveDirection;
    private Vector2 forceToApply;
    public float forceDamping;
    public float rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }

    void FixedUpdate()
    {
        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
        
        
        animator.SetFloat("Speed", moveDirection.magnitude);

    }

    void Move()
    {
        if (moveDirection == null || forceToApply == null)
        {
            return;
        }
        Vector2 moveForce = moveDirection * moveSpeed;
        moveForce += forceToApply;
        forceToApply /= forceDamping;
        if (Mathf.Abs(forceToApply.x) <= 0.01f && Mathf.Abs(forceToApply.y) <= 0.01f)
        {
            forceToApply = Vector2.zero;
        }
        rb.velocity = moveForce;

        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90f;

            rb.rotation = Mathf.LerpAngle(rb.rotation, angle, rotationSpeed * Time.fixedDeltaTime);
        }
    }
}
