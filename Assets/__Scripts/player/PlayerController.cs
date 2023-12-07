using System.Collections.Generic;
//using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Sizes { SMALL, MEDIUM, LARGE };

public class PlayerController : MonoBehaviour
{
    // Singleton, reference to player object
    public static GameObject player;
    public static PlayerController instance;

    // Size
    public Sizes currentSize { get; private set; }

    private bool isBig = false;
    private bool isSmall = false;

    public float offsetLanding = 0.05f;

    [Header("Size Switch")]
    public bool bigEnabled = true;
    public bool smallEnabled = true;

    // Player Controls
    float deacceleration   =   4;
    float acceleration     =   20;
    float maxSpeed         =   4;
    float speed;
    float velocityX;
    Vector2 moveInput;

    bool  isFacingRight    =   true;
    
    // Jump
    [Header("Jump Controls")]
    [SerializeField] float jumpBufferTime       =       0.1f;
    [SerializeField] float jumpHoldForce        =       5f;
    [SerializeField] float coyoteTime           =       0.15f;
    float jumpCutOff            =       0.1f;
    float jumpForce             =       6.0f;

    public bool isJumping              =       false;
    public bool jumpPressed            =       false;
    bool canJump                       =       true;

    float coyoteTimer;
    float jumpBufferTimer;
    public bool isBouncing;

    float timer;
    public bool startedJump = false;
    public bool hasLanded = false;

    // Ground Check
    [Header("Ground Check")]
    [SerializeField] Vector2 groundCheckRad;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask layerIsGround;

    // Players references
    [HideInInspector]public InputActionAsset actions;
    private DevButtons devButtons;
    private SizeStats sizeStats;
    private Rigidbody2D rb;
    private PlayerParticleEffect effects;
    private SquishAndSquash squishAndSquash;
    private RayCastHandler rayCastHandler;
    PlayerAudioHandler playerAudioHandler;

    // Velocity Magnitude
    private float currentMagnitude;
    private float prevMagnitude;
    //private float currentYVelocity;
    //private float prevYVelocity;

    private List<float> yVelocities = new List<float>();
    public int numberOfVelocitiesToRecord = 10;

    private void Awake()
    {
        if (instance != null) return;
        instance = this;
        player = gameObject;

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
        playerAudioHandler = GetComponent<PlayerAudioHandler>();    
        devButtons      =       GameManager.Instance.gameObject.GetComponent<DevButtons>();
        squishAndSquash =       GetComponentInChildren<SquishAndSquash>();
        effects         =       GetComponent<PlayerParticleEffect>();
        rayCastHandler  =       GetComponent<RayCastHandler>();
        rb              =       GetComponent<Rigidbody2D>();

        //originalStretchAmount = squishAndSquash.stretchAmount;

        currentSize = Sizes.MEDIUM;
        jumpBufferTimer = 0;
    }

    void FixedUpdate()
    {
        RecordYVelocity();
    }

    void Update()
    {
        if (startedJump)
        {
            timer += 0.1f;
        }
        if (!startedJump) 
            timer = 0;

        if (timer > 1 && IsGrounded())
        {
            hasLanded = true;
        }
        else
            hasLanded = false;

        if (hasLanded)
        {
            effects.CreateLandDust();
            startedJump = false;
            squishAndSquash.Squish();

        }



        MoveX();
        HandleCoyoteTime();

        if (canJump && jumpPressed)
        {
            Jump();
            jumpPressed = false;
        }

        #region SwitchHandlers
        //s/b-Enabled??
        if (isSmall && smallEnabled)
        {
            currentSize = Sizes.SMALL;
        }

        if (isBig && bigEnabled && rayCastHandler.largeCanChangeSize && (rayCastHandler.sideCheck) && rayCastHandler.diagonalCheck)
        {
            currentSize = Sizes.LARGE;
        }

        if ((!isBig && !isSmall) && rayCastHandler.smallCanChangeSize && (rayCastHandler.sideCheck) && rayCastHandler.diagonalCheck)
        {
            currentSize = Sizes.MEDIUM;

        }

        SwitchSize(currentSize);
        #endregion

        prevMagnitude = currentMagnitude;
        currentMagnitude = rb.velocity.magnitude;
    }

    private void SwitchSize(Sizes size)
    {
        List<float> statList = sizeStats.ReturnStats(size);

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

        /*Do a flip 
         * if (!isFacingRight && moveInput.x > 0)
            Flip();
        else if (isFacingRight && moveInput.x < 0)
            Flip();
        */

        velocityX = Mathf.Clamp(velocityX, -maxSpeed, maxSpeed);

        if (moveInput.x == 0 || (moveInput.x < 0 == velocityX > 0))
            velocityX *= 1 - deacceleration * Time.deltaTime;

        rb.velocity = new Vector2(velocityX, rb.velocity.y);
    }

    void Jump()
    {
        effects.CreateJumpDust();
        effects.StopLandDust();
        if (coyoteTimer > 0 && jumpBufferTimer > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            jumpBufferTimer = 0;
            isJumping = true;

            squishAndSquash.Squash();
            SquashCollisionHandler();

        }
        else if (!isJumping && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHoldForce);
        }

    }

    /// <summary>
    /// Checks for small spaces when in the current size to limit the stretch and make the player able 
    /// to fit easily through the gaps.
    /// </summary>
    private void SquashCollisionHandler()
    {
        //Colliosion handlar in tight spaces.
        float originalStretchAmount = squishAndSquash.stretchAmount;

        if (currentSize== Sizes.SMALL && !rayCastHandler.mediumCanChangeSize)
            squishAndSquash.stretchAmount = 0.0f;
        else
            squishAndSquash.stretchAmount = originalStretchAmount;
    }

    void HandleCoyoteTime()
    {
        if (IsGrounded())
        {
            coyoteTimer = coyoteTime;
            canJump = true;
            
            isJumping = false;
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
        return Physics2D.OverlapBox(groundCheck.position, groundCheckRad, 0, layerIsGround);
    }

    #endregion

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
    
    #region InputHanlder
    public void Move(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    void OnJumpStarted(InputAction.CallbackContext ctx)
    {
        startedJump = true;

        if (ctx.performed)
        {
            jumpBufferTimer = jumpBufferTime;
            jumpPressed = true;
        }
    }

    void OnJumpCanceled(InputAction.CallbackContext ctx)
    {
        jumpBufferTimer -= jumpBufferTime;
        if (!ctx.performed && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpCutOff);
            coyoteTimer = 0;
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

    internal float GetAbsoluteYVelocity()
    {
        for (int i = yVelocities.Count - 1; i >= 0; i--)
        {
            if (yVelocities[i] > 0.0001f)
            {
                return yVelocities[i];
            }
        }

        return 0f;
    }

    void RecordYVelocity()
    {
        yVelocities.Add(Mathf.Abs(rb.velocity.y));

        if (yVelocities.Count > numberOfVelocitiesToRecord)
        {
            yVelocities.RemoveAt(0);
        }
    }
}
