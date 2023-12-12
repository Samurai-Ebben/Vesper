using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Profiling;

public class ventMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    bool canMove = true;

    public LayerMask wayPoint;
    private Vector2 inputDirection;

    private Rigidbody2D rb;
    InputActionAsset actions;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

        rb.velocity = new Vector2(inputDirection.x, inputDirection.y) * moveSpeed;

    }
}
