using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ventMovement : MonoBehaviour
{

    public float moveSpeed = 5f; // Adjust as needed
    public LayerMask wallLayer; // LayerMask for collision detection with walls or obstacles
    private Vector2 inputDirection;

    void Update()
    {
        inputDirection.x = Input.GetAxisRaw("Horizontal");
        inputDirection.y = Input.GetAxisRaw("Vertical");

        Move();
    }

    void Move()
    {
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = currentPosition + new Vector3(inputDirection.x, inputDirection.y, 0f);

        // Check if the target position is not blocked by a wall or obstacle
        if (!Physics2D.OverlapPoint(targetPosition, wallLayer))
        {
            transform.position = Vector3.MoveTowards(currentPosition, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
}
