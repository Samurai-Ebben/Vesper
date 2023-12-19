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
    public bool canMoveHori;
    public bool canMoveVert;
    public bool canMove = false;
    public LayerMask wayPoint;
    Vector2 input;
    public Vector2 inputDirection;

    Transform player;
    //Transform lastPos;
    private Rigidbody2D rb;
    InputActionAsset actions;
    RayCastHandler rayCastHandler;


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
        if(canMoveHori && input.x != 0)
        input = ctx.ReadValue<Vector2>();
    }
    void OnMoveCancel(InputAction.CallbackContext ctx)
    {
        input = Vector2.zero;
    }

    void MoveBuffer()
    {
        if (input.x > 0)
        {
            inputDirection.x = input.x;
            inputDirection.y = 0;
        }
        if (canMoveVert && input.y !=  0)
        {
            inputDirection.y = input.y;
            inputDirection.x = 0;
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
    }

    void Move()
    {
        //if (!canMove) return;

        rb.gravityScale = 0;

        canMoveVert = rayCastHandler.smallDownIsFree || rayCastHandler.smallTopIsFree;
        canMoveHori = rayCastHandler.rightSide || rayCastHandler.leftSide;

        rb.velocity = new Vector2(inputDirection.x, inputDirection.y) * moveSpeed;
    }

    //Vector2 playerRelativeDirection()
    //{
    //    Vector3 direction = lastPos.position - player.position;
    //    Vector2 relativePosition = Vector2.zero;

    //    if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
    //    {
    //        relativePosition.x = (direction.x > 0) ? 1 : -1;
    //    }
    //    else
    //    {
    //        relativePosition.y = (direction.y > 0) ? 1 : -1;
    //    }

    //    return relativePosition;
    //}

    public void Reset()
    {
        inputDirection = Vector2.zero;
    }

    public void RegisterSelfToResettableManager()
    {
        ResettableManager.Instance?.RegisterObject(this);
    }
}
