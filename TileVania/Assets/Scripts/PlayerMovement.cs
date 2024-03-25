using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D rigidbody2D;

    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 20f;
    Animator animator;

    [SerializeField] float climbSpeed = 5f;

    CapsuleCollider2D bodyCollider2D;
    BoxCollider2D feetCollider2D;

    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);
    bool isAlive = true;
    float gravityScaleAtStart;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bodyCollider2D = GetComponent<CapsuleCollider2D>();
        //feetCollider2D = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = rigidbody2D.gravityScale;
    }

    void Update()
    {
        if (!isAlive)
        {
            return;
        }
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    void OnMove(InputValue inputValue)
    {
        if (!isAlive)
        {
            return;
        }
        moveInput = inputValue.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue inputValue)
    {
        if (!isAlive)
        {
            return;
        }
        if (!bodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if (inputValue.isPressed)
        {
            rigidbody2D.velocity += new Vector2(0f, jumpSpeed);
        }
    }
    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, rigidbody2D.velocity.y);
        rigidbody2D.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(rigidbody2D.velocity.x) > Mathf.Epsilon;
        animator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rigidbody2D.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rigidbody2D.velocity.x), 1f);
        }
    }

    void ClimbLadder()
    {
        if (!bodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            rigidbody2D.gravityScale = gravityScaleAtStart;
            animator.SetBool("isClimbing", false);
            return;
        }

        Vector2 climbVelocity = new Vector2(rigidbody2D.velocity.x, moveInput.y * climbSpeed);
        rigidbody2D.velocity = climbVelocity;
        rigidbody2D.gravityScale = 0f;

        bool playerVerticalSpeed = Mathf.Abs(rigidbody2D.velocity.y) > Mathf.Epsilon;
        animator.SetBool("isClimbing", playerVerticalSpeed);
    }

    void Die()
    {
        if (rigidbody2D.IsTouchingLayers(LayerMask.GetMask("Enemies")))
        {
            isAlive = false;
            animator.SetTrigger("Dying");
            rigidbody2D.velocity = deathKick;
        }
    }
}
