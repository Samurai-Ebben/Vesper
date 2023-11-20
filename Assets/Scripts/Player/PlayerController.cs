using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("||PLAYER CONTROLS||")]
    [SerializeField] float speed;
    [SerializeField] float maxSpeed = 4;
    [SerializeField] float acceleration = 20;
    [SerializeField] float deacceleration = 4;

    [Header("|Jumping Controls|")]
    [SerializeField] float maxJumps = 1;
    [SerializeField] float jumpBufferTime = 0.1f;
    [SerializeField] float jumpForce = 6.0f;
    [Range(0f, 1f)]
    [SerializeField] float jumpCutOff = 0.1f;
    [SerializeField] bool InJumpBuffer;

    [Header("|Air Controls|")]
    [SerializeField] float airAcceleration;
    [SerializeField] float airBrake;
    [SerializeField] float airControl;

    [Header("|Wall Controls|")]
    [SerializeField] float wallJumpingDuration = 0.4f;
    [SerializeField] float wallJumpingTime = 0.2f;
    [SerializeField] Vector2 wallJumpPower = new Vector2(8, 16);
    [SerializeField] float wallslidingSpeed = 1f;
    [SerializeField] float wallCheckRadius = .15f;

    //Controls
    float velocityX;
    float velocityWater;
    float moveInput;
    float inputY = 0;

    //Features
    float coyoteTimeCounter;
    float coyoteTime = 0.2f;

    //wallJumping
    float wallJumpingTimer;
    float wallJumpingDirection;
    bool isWallSliding = false;
    bool isWallJumping;

    float groundCheckRad = .15f;

    float jumpBufferCounter;
    float currJumps = 0;
    bool isFacingRight = true;

    [Header("||LAYERS||")]
    [SerializeField] private LayerMask isGround;
    [SerializeField] private LayerMask isWall;

    [Header("||REFRENCES||")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;

    //Players refrences
    Animator anim;
    private Rigidbody2D rb;

    #region EventHandlar
    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>().x;
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        InJumpBuffer = true;
    }

    public void OnJumpCancel(InputAction.CallbackContext ctx)
    {
        InJumpBuffer = false;

        if (rb.velocity.y < 0) return;

        rb.velocity = new(rb.velocity.x, rb.velocity.y * jumpCutOff);
    }
    #endregion
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        jumpBufferCounter = 0;

        anim = GetComponent<Animator>();

        speed = maxSpeed;
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        anim.SetBool("isMoving", moveInput != 0);


        speed = maxSpeed;

        Jumping();
        WallSlide();
        WallJump();

        if (!isWallJumping)
            if (isFacingRight && moveInput < 0f || !isFacingRight && moveInput > 0f)
                Flip();
    }

    private void Jumping()
    {
        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
            currJumps = 0;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
            jumpBufferCounter -= Time.deltaTime;

        if (Input.GetButtonDown("Jump") && currJumps < maxJumps)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            currJumps++;
        }

        if (coyoteTimeCounter > 0 && jumpBufferCounter > 0 /*&& !isJumping*/ )
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpBufferCounter = 0;
            currJumps = 0;
            //StartCoroutine(JumpCooldown());
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpCutOff);

            coyoteTimeCounter = 0;
        }

        if (rb.velocity.y < 0)
        {
            rb.gravityScale = 5.7f;
        }
        else
            rb.gravityScale = 1;
        MoveX(moveInput);
    }

    private void MoveX(float x)
    {
        anim.SetBool("isMoving", x != 0);


        //add our input to our velocity
        //This provides accelleration +10m/s/s
        velocityX += x * acceleration * Time.deltaTime;

        velocityX = Mathf.Clamp(velocityX, -speed, speed);
        //Check our max speed, if our magnitude is faster them max speed


        //If we have zero input from the player
        if (x == 0 || (x < 0 == velocityX > 0))
        {
            //Reduce our speed based on how fast we are going
            //A value of 0.9 would remove 10% or our speed
            velocityX *= 1 - deacceleration * Time.deltaTime;
        }


        //Now we can move with the rigidbody and we get propper collisions
        if (!isWallJumping)
            rb.velocity = new Vector2(velocityX, rb.velocity.y);
    }


    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRad, isGround);
    }

    bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, isWall);
    }

    void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && moveInput != 0)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallslidingSpeed, float.MaxValue));
        }
        else
            isWallSliding = false;
    }

    void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            currJumps = maxJumps;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingTimer = wallJumpingTime;
            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingTimer -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && isWallSliding/*wallJumpingTimer > 0*/)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpPower.x, wallJumpPower.y);
            wallJumpingTimer = 0;

            if (transform.localScale.x != wallJumpingDirection)
            {
                Flip();
            }
            Invoke(nameof(StopWallJumping), wallJumpingDuration);

        }
    }

    void StopWallJumping()
    {
        isWallJumping = false;
    }

    void UnderWaterControlls(float y)
    {
        if (y == 0 || (y < 0 == velocityWater > 0))
        {
            //Reduce our speed based on how fast we are going
            //A value of 0.9 would remove 10% or our speed
            velocityWater *= 1 - deacceleration * Time.deltaTime;
        }
        velocityWater = Mathf.Clamp(velocityWater, -speed, speed);

        velocityWater += y * acceleration * Time.deltaTime;

        rb.velocity = new Vector2(rb.velocity.x, velocityWater);
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRad);
        Gizmos.DrawWireSphere(wallCheck.position, wallCheckRadius);

        //Gizmos.DrawLine(transform.position)
    }
}
