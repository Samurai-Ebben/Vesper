using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Profiling;
using UnityEngine.Windows;

public class VentMovement : MonoBehaviour, IReset
{
    public float moveSpeed = 5f;
    public bool canMoveRight;
    public bool canMoveUp;
    public bool canMoveDown;
    public bool canMoveLeft;
    //public bool canMove = false;
    public LayerMask wayPoint;
    Vector2 input;
    public Vector2 inputDirection;

    Transform player;
    //Transform lastPos;
    private Rigidbody2D rb;
    InputActionAsset actions;
    RayCastHandler rayCastHandler;

    public Vector2 bufferedInput;
    public Vector3 prevPos;


    public float deadZoneY = 0.9f;
    private void Start()
    {
        rayCastHandler = GetComponent<RayCastHandler>();
        rb = GetComponent<Rigidbody2D>();
        player = PlayerController.player.transform;

        RegisterSelfToResettableManager();
    }

    private void OnEnable()
    {
        actions = GetComponent<PlayerInput>().actions;

        actions["Vent"].performed += OnMove;
        actions["Vent"].canceled += OnMoveCancel;

        actions.Enable();
        inputDirection = PlayerController.instance.moveInput;

    }

    private void OnDisable()
    {
        actions["Vent"].performed -= OnMove;
        actions["Vent"].canceled -= OnMoveCancel;
        input = Vector2.zero;
        //print("Direction" + input)
    }

    void Update()
    {
        MoveBuffer();
        Move();
    }

    void OnMove(InputAction.CallbackContext ctx)
    {
        input = ctx.ReadValue<Vector2>();
        MaxInput();
    }

    void MaxInput()
    {
        if(Mathf.Abs(input.x) > Mathf.Abs(input.y))
        {
            input.y = 0;
        }
        else if (Mathf.Abs(input.y) > Mathf.Abs(input.x))
        {
            input.x = 0;
        }
    }

    void OnMoveCancel(InputAction.CallbackContext ctx)
    {
        input = Vector2.zero;
    }

    void MoveBuffer()
    {
        if (input.x > deadZoneY)
        {
            if (canMoveRight)
            {
                inputDirection.x = 1;
                inputDirection.y = 0;
                bufferedInput = Vector2.zero;
                return;
            }
            else
            {
                bufferedInput.x = 1;
                bufferedInput.y = 0;
            }
        }

        if (input.x < -deadZoneY)
        {
            if (canMoveLeft)
            {
                inputDirection.x = -1;
                inputDirection.y = 0;
                bufferedInput = Vector2.zero;
                return;
            }
            else
            {
                bufferedInput.x = -1;
                bufferedInput.y = 0;
            }
        }

        if (input.y > deadZoneY)
        {
            if (canMoveUp)
            {
                inputDirection.y = 1;
                inputDirection.x = 0;
                bufferedInput = Vector2.zero;
                return;
            }
            else
            {
                bufferedInput.y = 1;
                bufferedInput.x = 0;
            }
        }

        if (input.y < -deadZoneY)
        {
            if (canMoveDown)
            {
                inputDirection.y = -1;
                inputDirection.x = 0;
                bufferedInput = Vector2.zero;
                return;
            }
            else
            {
                bufferedInput.y = -1;
                bufferedInput.x = 0;
            }
        }
        return;

    }

    void Move()
    {
        //if (!canMove) return;

        rb.gravityScale = 0;

        canMoveUp = rayCastHandler.smallTopIsFree;
        canMoveDown = rayCastHandler.smallDownIsFree;
        canMoveLeft = rayCastHandler.leftSide;
        canMoveRight = rayCastHandler.rightSide;

        if (bufferedInput != Vector2.zero)
        {
            if (((canMoveRight || canMoveLeft) && bufferedInput.x != 0) || ((canMoveUp || canMoveDown) && bufferedInput.y != 0))
            {
                rb.velocity = new Vector2(bufferedInput.x, bufferedInput.y) * moveSpeed;
            }
            else
            {
                rb.velocity = new Vector2(inputDirection.x, inputDirection.y) * moveSpeed;
            }
        }
        else
        {
            rb.velocity = new Vector2(inputDirection.x, inputDirection.y) * moveSpeed;
        }

        if (Vector3.Distance(transform.position, prevPos) < 0.005)
        {
            bufferedInput = Vector2.zero;
        }
        prevPos = transform.position;
    }

    public void Reset()
    {
        inputDirection = Vector2.zero;
    }

    public void RegisterSelfToResettableManager()
    {
        ResettableManager.Instance?.RegisterObject(this);
    }
}
