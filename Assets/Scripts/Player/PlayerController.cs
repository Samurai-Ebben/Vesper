using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("||PLAYER CONTROLS||")]
    [SerializeField] float speed;
    [SerializeField] float maxSpeed         = 4;
    [SerializeField] float acceleration     = 20;
    [SerializeField] float deacceleration   = 4;
    bool isFacingRight = true;

    //Controls
    float velocityX;
    Vector2 moveInput;

    [Header("|Jumping Controls|")]
    [SerializeField] float jumpBufferTime = 0.1f;
    [SerializeField] float jumpForce = 6.0f;
    [SerializeField] private float jumpHoldForce = 5f;
    [SerializeField] float jumpHeight = 6.0f;

    [SerializeField, Range(0f, 1f)] float jumpCutOff = 0.1f;

    [SerializeField] bool InJumpBuffer;
    [SerializeField]float coyoteTime = 0.2f;
    private float coyoteTimer;
    private float jumpBufferTimer;

    private bool isJumping = false;
    private bool canJump = true;
    private bool jumpPressed = false;

    [Header("|Air Controls|")]
    [SerializeField] float fallSpeed = 3.5f;

    [SerializeField]Vector2 groundCheckRad;
    [SerializeField]Vector2 sideGroundCheckRad;

    [Header("||SWITCH_CONTROLS||")]
    bool isSmall;
    bool isLarge;

    [Header("||LAYERS||")]
    [SerializeField] private LayerMask isGround;

    [Header("||REFRENCES||")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform sideGroundCheck;
    public GameObject groundHolderRight;
    public GameObject groundHolderLeft;

    //Players refrences
    private InputActionAsset actions;
    private DevButtons devBut;
    private SizeStats sizeStats;
    private Rigidbody2D rb;
    Transform origiParent;

    //private SwitchSize switchSize;
    public bool activeMovementScript;

    private void Awake()
    {
        //actions = GetComponentInParent<PlayerInput>().actions;
        actions = GetComponent<PlayerInput>().actions;
        //switchSize = GetComponentInParent<SwitchSize>();
        sizeStats = GetComponent<SizeStats>();

        actions["Move"].performed += Move;
        actions["Move"].canceled += Move;

        actions["Jump"].performed += OnJumpStarted;
        actions["Jump"].canceled += OnJumpCanceled;

        actions["Smaller"].started += Smaller;
        actions["Smaller"].canceled += SmallerCancel;
        actions["Larger"].started += Larger;
        actions["Larger"].canceled += LargerCancel;

        actions.Enable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        devBut = Camera.main.GetComponent<DevButtons>();
        origiParent = transform.parent;

        jumpBufferTimer = 0;

        //speed = maxSpeed;
    }

    void Update()
    {
        MoveX();
        //Jumping();
        HandleJumpBuffer();
        HandleCoyoteTime();

        if (canJump &&IsGrounded() && jumpPressed)
        {
            Jump();
            jumpPressed = false;
        }

        #region SwitchHandlers
        if (isSmall)
            SwitchSize("small");

        if (isLarge)
            SwitchSize("large");

        if (!isLarge && !isSmall)
        {
            SwitchSize("medium");
        }
        #endregion

        EdgeCheck();
    }

    private void SwitchSize(string sizeName)
    {
        List<float> statList = sizeStats.ReturnStats(sizeName);

        transform.localScale = new Vector3(statList[0], statList[0], statList[0]);
        maxSpeed = statList[1];
        acceleration = statList[2];
        deacceleration = statList[3];
        jumpForce = statList[4];
        rb.gravityScale = statList[5];
    }

    private void EdgeCheck()
    {
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

    //private void Jumping()
    //{
    //    //if (BesideGround() && IsGrounded()) return;
    //    //jumpForce = Mathf.Sqrt(jumpHeight * (Physics2D.gravity.y * rb.gravityScale) * -2) * rb.mass;

    //    if (IsGrounded())
    //        coyoteTimer = coyoteTime;
    //    else
    //        coyoteTimer -= Time.deltaTime;

    //    if (InJumpBuffer)
    //    {
    //        jumpBufferTimer = jumpBufferTime;
    //    }
    //    else
    //        jumpBufferTimer -= Time.deltaTime;

    //    //JumpBuffer.
    //    if (coyoteTimer > 0 && jumpBufferTimer > 0)
    //    {
    //        //rb.velocity = Vector2.up * jumpForce;
    //        //rb.velocity = new Vector2(rb.velocity.x,jumpForce);
    //        jumpBufferTimer = 0;
    //    }

    //    //FallFaster
    //    if (rb.velocity.y < 0 && !devBut.amGhost)
    //        rb.gravityScale = fallSpeed;

    //    else if(!devBut.amGhost)
    //        rb.gravityScale = 1f;
    //}

    void Jump()
    {
        if (IsGrounded())
        {
            //rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            rb.velocity = Vector2.up * jumpForce;
            isJumping = true;
            //OnJump.Invoke();
        }
        else if (!isJumping && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHoldForce);
        }
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

        //if (!isFacingRight && moveInput.x > 0)
        //    Flip();
        //else if (isFacingRight && moveInput.x < 0)
        //    Flip();

        velocityX = Mathf.Clamp(velocityX, -maxSpeed, maxSpeed);

        if (moveInput.x == 0 || (moveInput.x < 0 == velocityX > 0))
            velocityX *= 1 - deacceleration * Time.deltaTime;

        rb.velocity = new Vector2(velocityX, rb.velocity.y);
    }


    void HandleJumpBuffer()
    {
        if (jumpBufferTimer > 0 && jumpPressed)
        {
            jumpBufferTimer -= Time.deltaTime;
            if (jumpBufferTimer <= 0 && coyoteTimer > 0)
            {
                jumpPressed = true;
                canJump = false;
            }
        }
    }

    void HandleCoyoteTime()
    {
        if (!IsGrounded())
        {
            coyoteTimer -= Time.deltaTime;
        }
        else
        {
            coyoteTimer = coyoteTime;
            canJump = true;
        }
    }

    #region Checkers
    bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundCheck.position, groundCheckRad, 0, isGround);
    }

    bool BesideGround()
    {
        return Physics2D.OverlapBox(sideGroundCheck.position, sideGroundCheckRad, 0, isGround);
    }
    #endregion

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void SetParent(Transform newParent)
    {
        origiParent = transform.parent;
        transform.parent = newParent;
    }

    private void OnDisable()
    {
        actions["Move"].performed -= Move;
        actions["Move"].canceled -= Move;

        //actions["Jump"].performed -= OnJump;
        //actions["Jump"].canceled -= OnJump;
        actions["Jump"].performed -= OnJumpStarted;
        actions["Jump"].canceled -= OnJumpCanceled;

        #region switchControls
        actions["Smaller"].started -= Smaller;
        actions["Smaller"].canceled -= SmallerCancel;
        actions["Larger"].started -= Larger;
        actions["Larger"].canceled -= LargerCancel;
        #endregion

        actions.Disable();
    }

    #region EventHandler
    public void Move(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    //public void OnJump(InputAction.CallbackContext ctx)
    //{
    //    if (ctx.performed)
    //    {
    //        jumpBufferTimer = jumpBufferTime;
    //        if (canJump)
    //        {
    //            jumpPressed = true;
    //            canJump = false;
    //        }

    //    }
    //    if (ctx.canceled && rb.velocity.y>0)
    //    {
    //        rb.velocity = new(rb.velocity.x, rb.velocity.y * jumpCutOff);
    //    }
    //}

    void OnJumpStarted(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && canJump)
        {
            jumpBufferTimer = jumpBufferTime;
            jumpPressed = true;
            canJump = false;
        }
    }

    void OnJumpCanceled(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpCutOff);
        }
    }

    //public void OnJumpCancel(InputAction.CallbackContext ctx)
    //{
    //}

    public void Smaller(InputAction.CallbackContext ctx)
    {
        isSmall = true;
    }
    public void SmallerCancel(InputAction.CallbackContext ctx)
    {
        isSmall = false;
    }
    public void Larger(InputAction.CallbackContext ctx)
    {
        isLarge = true;
    }
    public void LargerCancel(InputAction.CallbackContext ctx)
    {
        isLarge = false;
    }
    #endregion

    private void OnDrawGizmosSelected()
    {
        //Gizmos.DrawWireSphere(groundCheck.position, groundCheckRad);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundCheck.position, groundCheckRad);
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(sideGroundCheck.position, sideGroundCheckRad);

        //Gizmos.DrawLine(transform.position)
    }
}
