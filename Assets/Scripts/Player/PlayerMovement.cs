using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    private Rigidbody2D rb;
    private Animator animator;
    public static PlayerMovement Instance { get; private set; }

    [Space]

    [Header("Values")]
    public bool y;
    private bool isGrounded;
    private float sideWalk;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float delay;

    void Start()
    {
        Instance = this;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        Move();
    }


    void Move()
    {
        sideWalk = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(sideWalk * moveSpeed, rb.linearVelocity.y);
        Vector2 newScale = transform.localScale;
        if (sideWalk < 0)
        {
            animator.SetBool("isRun", sideWalk != 0);
            newScale.x = -1;
        }
        if (sideWalk == 0)
        {
            animator.SetBool("isRun", false);
        }
        if (sideWalk > 0)
        {
            newScale.x = 1;
            animator.SetBool("isRun", sideWalk != 0);
        }
        transform.localScale = newScale;
    }

    public void Jump()
    {
        PlayerAttack.Instance.canAttack = false;
        isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.5f, 0.1f), CapsuleDirection2D.Horizontal, 0, groundLayer);
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            animator.SetBool("isJump", true);
            StartCoroutine(JumpToIdle());
        }
    }

    IEnumerator JumpToIdle()
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool("isJump", false);
        PlayerAttack.Instance.canAttack = true;
    }
}
