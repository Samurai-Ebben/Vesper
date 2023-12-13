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
    bool canMove = true;

    public LayerMask wayPoint;
    public Vector2 inputDirection {  get; private set; }

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
        inputDirection = ctx.ReadValue<Vector2>();
    }

    void Move()
    {
        rb.gravityScale = 0;
        Debug.Log(inputDirection);
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
