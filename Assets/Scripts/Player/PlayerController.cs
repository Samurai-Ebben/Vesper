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
    [SerializeField] float jumpBufferTime = 0.1f;
    [SerializeField] float jumpForce = 6.0f;
    [SerializeField] float jumpHeight = 3.0f;
    [Range(0f, 1f)]
    [SerializeField] float jumpCutOff = 0.1f;
    [SerializeField] bool InJumpBuffer;
    bool isJumping;

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
    float moveInput;

    //Features
    float coyoteTimeCounter;
    float coyoteTime = 0.2f;


    float groundCheckRad = .15f;

    float jumpBufferCounter;
    bool isFacingRight = true;

    [Header("||LAYERS||")]
    [SerializeField] private LayerMask isGround;
    [SerializeField] private LayerMask isWall;

    [Header("||REFRENCES||")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    private InputActionAsset actions;
    private DevButtons devBut;
    public GameObject groundHolderRight;
    public GameObject groundHolderLeft;

    //Players refrences
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
    private void Awake()
    {
        actions = GetComponent<PlayerInput>().actions;

        actions["Move"].performed += OnMove;
        actions["Move"].canceled += OnMove;

        actions["Jump"].started += OnJump;
        actions["Jump"].canceled += OnJumpCancel;

        actions.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        devBut = Camera.main.GetComponent<DevButtons>();
        jumpBufferCounter = 0;

        speed = maxSpeed;
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        print(moveInput);
        MoveX(moveInput);
        Jumping();

    }

    private void Jumping()
    {
        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
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

        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (coyoteTimeCounter > 0 && jumpBufferCounter > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpBufferCounter = 0;
            
        }
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpCutOff);

            coyoteTimeCounter = 0;
        }

        if (rb.velocity.y < 0 && !devBut.amGhost)
        {
            rb.gravityScale = 4f;
        }
        else if(!devBut.amGhost)
            rb.gravityScale = 1;
    }

    private void MoveX(float x)
    {
        velocityX += x * acceleration * Time.deltaTime;

        velocityX = Mathf.Clamp(velocityX, -maxSpeed, maxSpeed);
        if (x == 0 || (x < 0 == velocityX > 0))
            velocityX *= 1 - deacceleration * Time.deltaTime;

         rb.velocity = new Vector2(velocityX, rb.velocity.y);
    }


    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRad, isGround);
    }


    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    private void OnDisable()
    {
        actions["Move"].performed -= OnMove;
        actions["Move"].canceled -= OnMove;

        actions["Jump"].started -= OnJump;
        actions["Jump"].canceled -= OnJumpCancel;

        actions.Disable();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRad);
        Gizmos.DrawWireSphere(wallCheck.position, wallCheckRadius);

        //Gizmos.DrawLine(transform.position)
    }

    
}
