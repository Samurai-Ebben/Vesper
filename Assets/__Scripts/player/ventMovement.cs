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
    public bool canMoveHori;
    public bool canMoveVert;
    public LayerMask wayPoint;
    public Vector2 inputDirection;

    private Rigidbody2D rb;
    InputActionAsset actions;
    RayCastHandler rayCastHandler;

    private void Start()
    {
        rayCastHandler = GetComponent<RayCastHandler>();
        rb = GetComponent<Rigidbody2D>();
        RegisterSelfToResettableManager();
    }

    private void OnEnable()
    {
        actions = GetComponent<PlayerInput>().actions;

        actions["Move"].performed += OnMove;
        //actions["Move"].canceled += OnMove;
        actions.Enable();
    }

    private void OnDisable()
    {
        actions["Move"].performed -= OnMove;

        inputDirection = Vector2.zero;
        //actions.Disable();
    }

    void Update()
    {
        Move();
    }

    /*
    void OnMove(InputAction.CallbackContext ctx)
    {
        Vector2 input = ctx.ReadValue<Vector2>();
        if(canMoveHori && input.x != 0)
        {
            inputDirection.x = Mathf.Sign(input.x);
            inputDirection.y = 0;
        }
        if (canMoveVert && input.y != 0)
        {
            inputDirection.x = 0;
            inputDirection.y = Mathf.Sign(input.y);
        }
    }*/

    void OnMove(InputAction.CallbackContext ctx)
    {
        Vector2 input = ctx.ReadValue<Vector2>();

        // Check if currently moving horizontally or vertically
        bool movingHorizontally = Mathf.Abs(inputDirection.x) > Mathf.Abs(inputDirection.y);
        bool movingVertically = Mathf.Abs(inputDirection.y) > Mathf.Abs(inputDirection.x);

        if (canMoveHori && input.x != 0 && !movingVertically)
        {
            inputDirection.x = Mathf.Sign(input.x);
            inputDirection.y = 0;
        }
        if (canMoveVert && input.y != 0 && !movingHorizontally)
        {
            inputDirection.x = 0;
            inputDirection.y = Mathf.Sign(input.y);
        }
    }

    void Move()
    {
        inputDirection = inputDirection.normalized;

        rb.gravityScale = 0;
        canMoveVert = rayCastHandler.smallDownIsFree || rayCastHandler.smallTopIsFree;
        canMoveHori = rayCastHandler.rightSide || rayCastHandler.leftSide;

        rb.velocity = new Vector2(inputDirection.x, inputDirection.y) * moveSpeed;

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
