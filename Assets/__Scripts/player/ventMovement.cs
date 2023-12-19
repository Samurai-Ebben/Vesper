using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Profiling;

public class VentMovement : MonoBehaviour, IReset
{
    public float moveSpeed = 5f;
    public bool canMoveRight;
    public bool canMoveUp;
    public bool canMoveDown;
    public bool canMoveLeft;
    //public bool canMove = false;
    public LayerMask wayPoint;
    public Vector2 inputDirection;

    Transform player;
    //Transform lastPos;
    private Rigidbody2D rb;
    InputActionAsset actions;
    RayCastHandler rayCastHandler;

    public Vector2 bufferedInput;
    public Vector3 prevPos;

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
        
        actions.Enable();
        inputDirection = PlayerController.instance.moveInput;

    }

    private void OnDisable()
    {
        actions["Vent"].performed -= OnMove;
        inputDirection = Vector2.zero;
    }

    void Update()
    {
        Move();
    }

    void OnMove(InputAction.CallbackContext ctx)
    {
        Vector2 input = ctx.ReadValue<Vector2>();

        if (input.x > 0)
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

        if (input.x < 0)
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

        if (input.y > 0)
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

        if (input.y < 0)
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

        // if(canMoveHor && input.x != 0)
        //{
        //    inputDirection.x = input.x;
        //    inputDirection.y = 0;
        //}
        // if (canMoveVert && input.y !=  0)
        //{
        //    inputDirection.y = input.y;
        //    inputDirection.x = 0;
        //}
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
            if(((canMoveRight || canMoveLeft) && bufferedInput.x != 0) || ((canMoveUp || canMoveDown) && bufferedInput.y != 0))
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

        if(Vector3.Distance(transform.position, prevPos) < 0.005)
        {
            bufferedInput = Vector2.zero;
        }
        prevPos = transform.position;
        print(prevPos);
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
