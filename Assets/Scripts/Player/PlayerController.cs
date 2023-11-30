using System;
using System.Collections;
using System.Collections.Generic;
//using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    RayCastHandler rayCastHandler;

    [Header("||PLAYER CONTROLS||")]
    [SerializeField] float deacceleration   =   4;
    [SerializeField] float acceleration     =   20;
    [SerializeField] float maxSpeed         =   4;
    [SerializeField] float speed;
    Vector2 moveInput;
    [HideInInspector]public float velocityX;

    bool                   isFacingRight    =   true;

    [Header("|Jumping Controls|")]
    [SerializeField, Range(0f, 1f)] float jumpCutOff = 0.1f;
    [SerializeField] float jumpBufferTime       =       0.1f;
    [SerializeField] float jumpHoldForce        =       5f;
    [SerializeField] float jumpHeight           =       6.0f;
    [SerializeField] float jumpForce            =       6.0f;
    [SerializeField]float coyoteTime            =       0.2f;

    private bool isJumping      =       false;
    private bool canJump        =       true;
    private bool jumpPressed    =       false;

    private float coyoteTimer;
    [SerializeField] private float jumpBufferTimer;
    public bool isBouncing;

    [Header("|Air Controls|")]
    [SerializeField] float fallSpeed = 3.5f;

    [SerializeField]Vector2 groundCheckRad;
    //[SerializeField]Vector2 sideGroundCheckRad; // Make many raycasts instead.
    
    [Header("||LAYERS||")]
    [SerializeField] private LayerMask isGround;

    [Header("||REFRENCES||")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform sideGroundCheck;

    public GameObject groundHolderRight;
    public GameObject groundHolderLeft;

    //Players refrences
    private InputActionAsset actions;
    private DevButtons devButtons;
    private SizeStats sizeStats;
    private Rigidbody2D rb;

    //Sizes
    public enum Sizes { SMALL, MEDIUM, LARGE };
    public Sizes currentSize { get; private set; }

    //Velocity Magnitude
    private float currentMagnitude;
    private float prevMagnitude;
    public float deltaMagnitude;

    bool isBig = false;
    bool isSmall = false;

    private void Awake()
    {
        actions = GetComponent<PlayerInput>().actions;
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
        devButtons = FindObjectOfType<DevButtons>();
        rayCastHandler = GetComponent<RayCastHandler>();

        currentSize = Sizes.MEDIUM;
        jumpBufferTimer = 0;
    }

    void Update()
    {
        MoveX();
        HandleCoyoteTime();
        

        if (canJump && jumpPressed)
        {
            Jump();
            jumpPressed = false;
        }

        #region SwitchHandlers
        if (isSmall)
            currentSize = Sizes.SMALL;

        if (isBig)
            currentSize = Sizes.LARGE;

        if (!isBig && !isSmall)
        {
            currentSize = Sizes.MEDIUM;
        }
        if (currentSize == Sizes.SMALL)
            SwitchSize("small");

        if (currentSize == Sizes.LARGE && rayCastHandler.smallCanChangeSize && rayCastHandler.mediumCanChangeSize)
            SwitchSize("large");

        if (currentSize == Sizes.MEDIUM && rayCastHandler.smallCanChangeSize)
            SwitchSize("medium");
        #endregion

        prevMagnitude = currentMagnitude;
        currentMagnitude = rb.velocity.magnitude;
        deltaMagnitude = currentMagnitude - prevMagnitude;
    }

    private void SwitchSize(string sizeName)
    {
        List<float> statList = sizeStats.ReturnStats(sizeName);

        transform.localScale = new Vector3(statList[0], statList[0], statList[0]);
        maxSpeed                =       statList[1];
        acceleration            =       statList[2];
        deacceleration          =       statList[3];
        jumpForce               =       statList[4];
        rb.gravityScale         =       statList[5];
        jumpCutOff              =       statList[6];
        groundCheckRad.x        =       statList[7];
        groundCheckRad.y        =       statList[8];

    }

    private void MoveX()
    {
        if (isBouncing && !IsGrounded()) return;

        velocityX += moveInput.x * acceleration * Time.deltaTime;
        if (devButtons != null)
        {
            if (devButtons.amGhost)
            {
                float velocityY = 0;
                velocityY += moveInput.y * acceleration;
                rb.velocity = new Vector2(velocityX, velocityY);
            }
        }

        //if (!isFacingRight && moveInput.x > 0)
            //Flip();
        //else if (isFacingRight && moveInput.x < 0)
            //Flip();

        velocityX = Mathf.Clamp(velocityX, -maxSpeed, maxSpeed);

        if (moveInput.x == 0 || (moveInput.x < 0 == velocityX > 0))
            velocityX *= 1 - deacceleration * Time.deltaTime;

        rb.velocity = new Vector2(velocityX, rb.velocity.y);
    }

    void Jump()
    {
        if (coyoteTimer > 0 && jumpBufferTimer > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            jumpBufferTimer = 0;
            isJumping = true;
            //canJump = true;
        }
        else if (!isJumping && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHoldForce);
        }
           
        //canJump = false;
    }

    void HandleCoyoteTime()
    {
        if (IsGrounded())
        {
            coyoteTimer = coyoteTime;
            canJump = true;
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
            if(coyoteTimer <= 0)
                canJump = false;

        }
    }

    #region Checkers
    public bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundCheck.position, groundCheckRad, 0, isGround);
    }

    #endregion

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
    
    #region InputHanldar
    public void Move(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    void OnJumpStarted(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            jumpBufferTimer = jumpBufferTime;
            jumpPressed = true;
            //canJump = true;
        }
    }

    void OnJumpCanceled(InputAction.CallbackContext ctx)
    {
        jumpBufferTimer -= jumpBufferTime;
        if (!ctx.performed && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpCutOff);
            coyoteTimer = 0;
            //canJump = true;
        }
    }

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
        isBig = true;
    }
    public void LargerCancel(InputAction.CallbackContext ctx)
    {
        isBig = false;
    }
    private void OnDisable()
    {
        actions["Move"].performed -= Move;
        actions["Move"].canceled -= Move;

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
    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundCheck.position, groundCheckRad);
        Gizmos.color = Color.red;
    }

    internal float GetMagnitude()
    {
        if (prevMagnitude != 0) return prevMagnitude;
        else return currentMagnitude;
    }
}
