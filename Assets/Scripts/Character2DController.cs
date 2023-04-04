using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character2DController : MonoBehaviour
{
    const string STAR = "star";
    const string KEY = "key";

    [Header("Movement")]
    [SerializeField]
    float walkSpeed = 500.0F;

    [SerializeField]
    [Range(0.1F,0.3F)]
    float smoothSpeed = 0.3F;

    [SerializeField]
    bool isFacingRight = true;

    [Header("Jump")]
    [SerializeField]
    float jumpForce = 140.0F;

    [SerializeField]
    float fallMultiplier = 5.0F;

    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    LayerMask groundMask;

    [SerializeField]
    float jumpGraceTime = 0.25F;

    [Header("Animation")]
    [SerializeField]
    Animator animator;

    IDictionary<string, int> collectibles;

    public UnityEvent<int> OnStartCountChanged;
    public UnityEvent<string, int> OnInteract;

    Rigidbody2D rb;

    Vector2 gravity;
    Vector2 velocityZero = Vector2.zero;

    float inputX;
    float lastTimeJumpPreseed;

    bool isMoving;
    bool isJumpPressed;

     void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gravity = -Physics2D.gravity;

        collectibles = new Dictionary<string, int>()
        {
            {STAR, 0 },
            {KEY, 0}
        };
        
    }

    void Update()
    {
        HandleInputs();
    }
    private void FixedUpdate()
    {
        HandleJump();
        HandleMove();
        HandleFlipX();
    }

    void HandleInputs()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        isMoving = inputX != 0.0F;

        isJumpPressed = Input.GetButtonDown("Jump");
        if (isJumpPressed)
        {
            lastTimeJumpPreseed = Time.time;
        }
    }

    void HandleMove()
    {
        float velocityX = inputX * walkSpeed * Time.fixedDeltaTime;
        animator.SetFloat("speed", MathF.Abs(velocityX));

        Vector2 direction = new Vector2(velocityX, rb.velocity.y);
        //rb.velocity = direction;

        rb.velocity = Vector2.SmoothDamp(rb.velocity, direction, ref velocityZero, smoothSpeed);
    }

    void HandleFlipX()
    {
        if (isMoving)
        {
            bool facingRight = inputX > 0.0F;
            if(isFacingRight != facingRight)
            {
                isFacingRight = facingRight;
                transform.Rotate(0.0F, 180.0F, 0.0F);

                //Vector2 localScale = transform.localScale;
                //localScale.x *= -1;
                //transform.localScale = localScale;
            }
        }
    }  
    
    void HandleJump()
    {
        if(lastTimeJumpPreseed > 0.0F && Time.time - lastTimeJumpPreseed <= jumpGraceTime)
        {
            isJumpPressed = true;
        }
        else
        {
            lastTimeJumpPreseed = 0.0F;
        }

        if (isJumpPressed && IsGrounded())
        {
            rb.velocity += Vector2.up * jumpForce * Time.fixedDeltaTime;
            return;
        }

        if(rb.velocity.y < -0.1F)
        {
            rb.velocity -= gravity * fallMultiplier * Time.fixedDeltaTime;
        }
    }

    bool IsGrounded()
    {
        return
            Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.63F, 0.04F),
                CapsuleDirection2D.Horizontal, 0.0F, groundMask);
    }

    public void IncraseCollectible(string collectibleType, int value)
    {
        collectibleType = collectibleType.ToLower();

        if (!collectibles.ContainsKey(collectibleType))
        {
            return;
        }

        collectibles[collectibleType] += value;
        switch (collectibleType)
        {
            case STAR: 
                if(OnStartCountChanged != null)
                {
                    OnStartCountChanged.Invoke(collectibles[collectibleType]);
                }
                break;
        }
    }

    public bool ContainsCollectible(string collectibleType, int value = 1)
    {
        collectibleType = collectibleType.ToLower();

        if (!collectibles.ContainsKey(collectibleType))
        {
            return false;
        }

        return collectibles[collectibleType] >= value;
    }

    public void ReduceCollectible(string collectibleType, int value = 1)
    {
        collectibleType = collectibleType.ToLower();

        if (!collectibles.ContainsKey(collectibleType))
        {
            return;
        }

        collectibles[collectibleType] -= value;

        switch (collectibleType)
        {
            case KEY:
                if (OnInteract != null)
                {
                    OnInteract.Invoke(collectibleType, collectibles[collectibleType]);
                }
                break;
        }
    }
}
