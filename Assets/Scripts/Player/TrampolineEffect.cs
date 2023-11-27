using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineEffect : MonoBehaviour
{
    private bool isOnTrampoline = false;
    PlayerController playerController;
    Rigidbody2D rb;
    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Trampoline"))
        {
            isOnTrampoline = true;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Trampoline"))
        {
            isOnTrampoline = false;

        }
    }

    private void FixedUpdate()
    {
        if (isOnTrampoline)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        } 
        else rb.velocity = new Vector2(playerController.velocityX, rb.velocity.y);

    }
}
