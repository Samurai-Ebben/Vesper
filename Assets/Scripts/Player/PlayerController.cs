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
    bool isFacingRight = true;
    //Controls
    float velocityX;
    Vector2 moveInput;

    [Header("|Jumping Controls|")]
    [SerializeField] float jumpBufferTime = 0.1f;
    [SerializeField] float jumpForce = 6.0f;
    [SerializeField] float jumpHeight = 3.0f;
    [SerializeField, Range(0f, 1f)] float jumpCutOff = 0.1f;
    [SerializeField] bool InJumpBuffer;
    [SerializeField]float coyoteTime = 0.2f;
    float coyoteTimeCounter;
    bool isJumping;
    float jumpBufferCounter;

    [Header("|Air Controls|")]
    [SerializeField] float airAcceleration;
    [SerializeField] float airBrake;
    [SerializeField] float airControl;

    [SerializeField]Vector2 groundCheckRad;
    [SerializeField]Vector2 sideGroundCheckRad;

    [Header("||SWICH_CONTROLS||")]
    bool isSmall;
    bool isLarge;

    [Header("||LAYERS||")]
    [SerializeField] private LayerMask isGround;

    [Header("||REFRENCES||")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform sideGroundCheck;
    public GameObject groundHolderRight;
    public GameObject groundHolderLeft;

    private InputActionAsset actions;
    private DevButtons devBut;
    private SwitchSize switchSize;

    //Players refrences
    private Rigidbody2D rb;

    #region EventHandlar
    public void Move(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
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
    public void Smaller(InputAction.CallbackContext ctx)
    {
        isSmall = true;
    }
    public void SmallerCancle(InputAction.CallbackContext ctx)
    {
        isSmall = false;
    }
    public void Larger(InputAction.CallbackContext ctx)
    {
        isLarge = true;
    }
    public void LargerCancle(InputAction.CallbackContext ctx)
    {
        isLarge = false;
    }
    #endregion
    private void Awake()
    {
        actions = GetComponentInParent<PlayerInput>().actions;
        switchSize = GetComponentInParent<SwitchSize>();

        actions["Move"].performed += Move;
        actions["Move"].canceled += Move;

        actions["Jump"].started += OnJump;
        actions["Jump"].canceled += OnJumpCancel;

        actions["Smaller"].started += Smaller;
        actions["Smaller"].canceled += SmallerCancle;
        actions["Larger"].started += Larger;
        actions["Larger"].canceled += LargerCancle;

        actions.Enable();
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
        MoveX();
        Jumping();
        switchSize.isSmall = isSmall;
        switchSize.isBig = isLarge;
        //Edge standing
        if (IsGrounded() && !BesideGround())
        {
            groundHolderLeft.SetActive(true);
            groundHolderRight.SetActive(true);
        }
        else
        {
            groundHolderLeft.SetActive(false);
            groundHolderRight.SetActive(false);
        }
    }

    private void Jumping()
    {
        //if (BesideGround() && IsGrounded()) return;

        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (InJumpBuffer)
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
            jumpBufferCounter -= Time.deltaTime;

        if (InJumpBuffer && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (coyoteTimeCounter > 0 && jumpBufferCounter > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpBufferCounter = 0;
        }

        if (!InJumpBuffer && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpCutOff);

            coyoteTimeCounter = 0;
        }

        if (rb.velocity.y < 0 && !devBut.amGhost)
        {
            rb.gravityScale = 3.5f;
        }
        else if(!devBut.amGhost)
            rb.gravityScale = 1;
    }

    private void MoveX()
    {
        velocityX += moveInput.x * acceleration * Time.deltaTime;
        if (devBut.amGhost)
        {
            float velocityY = 0;
            velocityY += moveInput.y * acceleration;
            rb.velocity = new Vector2(velocityX, velocityY);

        }

        if (!isFacingRight && moveInput.x > 0)
            Flip();
        else if (isFacingRight && moveInput.x < 0)
            Flip();

        velocityX = Mathf.Clamp(velocityX, -maxSpeed, maxSpeed);

        if (moveInput.x == 0 || (moveInput.x < 0 == velocityX > 0))
            velocityX *= 1 - deacceleration * Time.deltaTime;

         rb.velocity = new Vector2(velocityX, rb.velocity.y);
    }


    bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundCheck.position, groundCheckRad, 0, isGround);
    }

    bool BesideGround()
    {
        return Physics2D.OverlapBox(sideGroundCheck.position, sideGroundCheckRad, 0, isGround);
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
        actions["Move"].performed -= Move;
        actions["Move"].canceled -= Move;

        actions["Jump"].started -= OnJump;
        actions["Jump"].canceled -= OnJumpCancel;

        actions["Smaller"].started -= Smaller;
        actions["Smaller"].canceled -= SmallerCancle;
        actions["Larger"].started -= Larger;
        actions["Larger"].canceled -= LargerCancle;
        actions.Disable();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(groundCheck.position, groundCheckRad);

        Gizmos.DrawWireCube(groundCheck.position, groundCheckRad);
        Gizmos.DrawWireCube(sideGroundCheck.position, sideGroundCheckRad);

        //Gizmos.DrawLine(transform.position)
    }

    
}
