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

    public float landingSFX = 0.05f;

    [Header("Size Switch")]
    public bool bigEnabled = true;
    public bool smallEnabled = true;

    // Player Controls
    [SerializeField] float deacceleration   =   4;
    [SerializeField] float acceleration     =   20;
    [SerializeField] float maxSpeed         =   4;
    [SerializeField] float velocityX;
    float speed;
    Vector2 moveInput;

    bool  isFacingRight    =   true;
    
    // Jump
    [Header("Jump Controls")]
    float jumpBufferTime        =       0.1f;
    float jumpHoldForce         =       5f;
    float coyoteTime            =       0.15f;
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
    public bool hasLanded   = false;

    [Header("AirControls")]
    public bool inAir           =       false;
    float airSpeedMultiplier    =        .9f;
    float airAccMultiplier      =        .9f;
    float airDecMultiplier      =        .9f;

    // Ground Check
    [Header("Ground Check")]
    [SerializeField] Vector2 groundCheckRadius;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask layerIsGround;

    // Players references
    public  Rigidbody2D rb { get; private set; }
    [HideInInspector] public InputActionAsset actions;
    DevButtons devButtons;
    SizeStats sizeStats;
    PlayerParticleEffect effects;
    SquishAndSquash squishAndSquash;
    RayCastHandler rayCastHandler;
    ScreenShakeHandler screenShake;
    PlayerAudioHandler playerAudioHandler;
    AudioManager audioManager;

    // Velocity Magnitude
    private float currentMagnitude;
    private float prevMagnitude;

    private List<float> yVelocities = new List<float>();
    public int numberOfVelocitiesToRecord = 10;


    public bool pausedPressed = false;
    private void Awake()
    {
        if (instance != null) return;
        instance = this;
        player = gameObject;

        actions = GetComponent<PlayerInput>().actions;
        sizeStats = GetComponent<SizeStats>();

        actions["Move"].performed += Move;
        actions["Move"].canceled += Move;

        actions["Pause"].performed += OnPause;
        actions["Pause"].canceled += OnPauseCancel;
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
        devButtons          =     GameManager.Instance.gameObject.GetComponent<DevButtons>();
        playerAudioHandler  =     GetComponent<PlayerAudioHandler>();    
        squishAndSquash     =     GetComponentInChildren<SquishAndSquash>();
        effects             =     GetComponent<PlayerParticleEffect>();
        rayCastHandler      =     GetComponent<RayCastHandler>();
        rb                  =     GetComponent<Rigidbody2D>();
        screenShake         =     FindAnyObjectByType<ScreenShakeHandler>();
        playerAudioHandler =      FindAnyObjectByType<PlayerAudioHandler>();

        //originalStretchAmount = squishAndSquash.stretchAmount;

        currentSize = Sizes.MEDIUM;
        jumpBufferTimer = 0;
    }

    void FixedUpdate()
    {
        MoveX();
        Jump();
        CoyoteTime();
        RecordYVelocity();
        RecordMagnitude();
    }

    void Update()
    {
        JuiceFx();
        SwitchSize();
    }

    private void JuiceFx()
    {

        if (!IsGrounded())
        {
            timer += Time.deltaTime;
        }
        if(timer > landingSFX && IsGrounded())
        {

            timer = 0;
            LandingActions();
        }

        
    }
    private void SwitchSize()
    {
        if (isSmall && smallEnabled)
        {
            currentSize = Sizes.SMALL;
        }

        if (isBig && bigEnabled && rayCastHandler.largeTopIsFree && (rayCastHandler.anySide) && rayCastHandler.diagonalTop)
        {
            currentSize = Sizes.LARGE;
        }

        if ((!isBig && !isSmall) && rayCastHandler.smallTopIsFree && (rayCastHandler.anySide) && rayCastHandler.diagonalTop)
        {
            currentSize = Sizes.MEDIUM;
        }

        List<float> statList = sizeStats.ReturnStats(currentSize);

        transform.localScale    = new Vector3(statList[0], statList[0], statList[0]);
        maxSpeed                =       statList[1];
        acceleration            =       statList[2];
        deacceleration          =       statList[3];
        jumpForce               =       statList[4];
        rb.gravityScale         =       statList[5];
        jumpCutOff              =       statList[6];
        groundCheckRadius.x     =       statList[7];
        groundCheckRadius.y     =       statList[8];
        airSpeedMultiplier      =       statList[9];
        airAccMultiplier        =       statList[10];
        airDecMultiplier        =       statList[11];
        landingSFX              =       statList[12];


    }

    private void MoveX()
    {
        if (isBouncing) return;

        if (inAir)
        {
            maxSpeed *= airSpeedMultiplier;
            acceleration *= airAccMultiplier;
            //deacceleration *= airDecMulti;
        }
        else
        {
            maxSpeed /= airSpeedMultiplier;
            acceleration /= airAccMultiplier;
        }

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

        velocityX = Mathf.Clamp(velocityX, -maxSpeed, maxSpeed);

        if (moveInput.x == 0 || (moveInput.x < 0 == velocityX > 0))
        {
            if (inAir)
            {
                deacceleration *= airDecMultiplier;
            }
            velocityX *= 1 - deacceleration * Time.deltaTime;

            if (rb.velocity.magnitude < 0.1f)
            {
                velocityX = 0;
            }
        }
            
        rb.velocity = new Vector2(velocityX, rb.velocity.y);
    }

    void Jump()
    {
        if (!canJump || !jumpPressed) return;

        effects.CreateJumpDust();
        effects.StopLandDust();
        SquashCollisionHandler();
        playerAudioHandler.PlayJumpingSound();

        if (coyoteTimer > 0 && jumpBufferTimer > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            jumpBufferTimer = 0;
            isJumping = true;
            inAir = true;
            squishAndSquash.Squash();

        }
        else if (!isJumping && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHoldForce);
        }

        jumpPressed = false;
    }

    /// <summary>
    /// Checks for small spaces when in the current size to limit the stretch and make the player able 
    /// to fit easily through the gaps.
    /// </summary>
    private void SquashCollisionHandler()
    {
        //Colliosion handlar in tight spaces.
        float originalStretchAmount = squishAndSquash.stretchAmount;

        if (currentSize== Sizes.SMALL && !rayCastHandler.mediumTopIsFree)
            squishAndSquash.stretchAmount = 0.0f;
        else
            squishAndSquash.stretchAmount = originalStretchAmount;
    }

    void CoyoteTime()
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
        return Physics2D.OverlapBox(groundCheck.position, groundCheckRadius, 0, layerIsGround);
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

    public void OnPause(InputAction.CallbackContext ctx)
    {
        pausedPressed = true;
    }
    public void OnPauseCancel(InputAction.CallbackContext ctx)
    {
        pausedPressed = false;
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



        actions["Pause"].performed -= OnPause;
        actions["Pause"].canceled -= OnPauseCancel;

        actions.Disable();
    }
    #endregion

    private void RecordMagnitude()
    {
        prevMagnitude = currentMagnitude;
        currentMagnitude = rb.velocity.magnitude;
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundCheck.position, groundCheckRadius);
        Gizmos.color = Color.red;
    }

    private void LandingActions()
    {
        Debug.Log("Landing");
        effects.CreateLandDust();
        playerAudioHandler.PlayLandingSound();
        squishAndSquash.Squish();

            effects.CreateLandDust();
            startedJump = false;
            squishAndSquash.Squish();
            inAir = false;

            if (currentSize == Sizes.LARGE)
            {
                screenShake.JumpShake();
            }
    }
}

