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

    void OnMove(InputAction.CallbackContext ctx)
    {
        if(canMoveHori && ctx.ReadValue<Vector2>().x != 0)
        {
            inputDirection.x = ctx.ReadValue<Vector2>().x;
            inputDirection.y = 0;
        }
        if (canMoveVert && ctx.ReadValue<Vector2>().y != 0)
        {
            inputDirection.y = ctx.ReadValue<Vector2>().y;
            inputDirection.x = 0;

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
