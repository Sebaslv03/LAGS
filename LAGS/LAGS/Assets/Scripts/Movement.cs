using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    private Vector2 moveDirection = Vector2.zero;
    private Vector2 forceToApply = Vector2.zero;
    public float forceDamping;
    public float rotationSpeed;
    public AudioSource audioSource;

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

        if(moveDirection.magnitude > 0.01)
        {
            audioSource.enabled = true;
        }
        else
        {
            audioSource.enabled = false;
        }
        animator.SetFloat("Speed", moveDirection.magnitude);
        animator.SetFloat("Horizontal", moveDirection.x);
        animator.SetFloat("Vertical", moveDirection.y);


    }

    void Move()
    {
        Vector2 moveForce = moveDirection * moveSpeed;
        moveForce += forceToApply;
        forceToApply /= forceDamping;
        if (Mathf.Abs(forceToApply.x) <= 0.01f && Mathf.Abs(forceToApply.y) <= 0.01f)
        {
            forceToApply = Vector2.zero;
        }
        rb.velocity = moveForce;
        spriteRenderer.flipX = rb.velocity.x < 0;
    }
}
