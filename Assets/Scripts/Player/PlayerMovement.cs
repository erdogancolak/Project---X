using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }

    [Header("References")]
    private Rigidbody2D rb;
    private Animator animator;
    private Collider2D playerCollider;

    public Transform groundCheck;
    public LayerMask groundLayer;
    
    [Space]

    [Header("Settings")]

    [HideInInspector] public bool canMove;

    private bool isGrounded;

    private float sideWalk;

    [SerializeField] private float moveSpeed;
    [SerializeField] private bool isJumping;
    [SerializeField] private float jumpForce;
    [SerializeField] private float delay;

    [SerializeField] private bool isSliding = false;
    [SerializeField] private float slideSpeed;
    [SerializeField] private float slideDuration;

    public static bool isRight;
    void Start()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerCollider = GetComponent<Collider2D>();
        canMove = true;
    }


    void FixedUpdate()
    {
        Move();
    }


    void Move()
    {
        if (canMove)
        {
            sideWalk = Input.GetAxis("Horizontal");
            rb.linearVelocity = new Vector2(sideWalk * moveSpeed, rb.linearVelocity.y);

            Vector2 newScale = transform.localScale;
            if (sideWalk < 0)
            {
                isRight = false;
                animator.SetBool("isRun", sideWalk != 0);
                newScale.x = -1;
            }
            if (sideWalk == 0)
            {
                animator.SetBool("isRun", false);
            }
            if (sideWalk > 0)
            {
                isRight = true;
                newScale.x = 1;
                animator.SetBool("isRun", sideWalk != 0);
            }
            transform.localScale = newScale;
        }
    }

    public void Jump()
    {
        if (isSliding || isJumping) return;

        PlayerAttack.Instance.canAttack = false;
        isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.5f, 0.1f), CapsuleDirection2D.Horizontal, 0, groundLayer);

        if (isGrounded && isSliding == false)
        {
            isJumping = true;
            animator.SetBool("isJump", true);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            StartCoroutine(JumpToIdle());
        }
    }

    IEnumerator JumpToIdle()
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool("isJump", false);
        PlayerAttack.Instance.canAttack = true;
        isJumping = false;
    }

    public void Slide()
    {
        if (isSliding || isJumping) return;

        StartCoroutine(SlideIE());
    }

    IEnumerator SlideIE()
    {
        isSliding = true;
        playerCollider.enabled = false;
        float originalSpeed = moveSpeed;
        moveSpeed = slideSpeed;
        animator.SetBool("isSlide", true);

        yield return new WaitForSeconds(slideDuration);

        animator.SetBool("isSlide", false);
        playerCollider.enabled = true;
        moveSpeed = originalSpeed;

        isSliding = false;
    }
}
