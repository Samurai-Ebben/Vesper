using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentManager : MonoBehaviour
{
    PlayerController player;
    public float exitingSpeedX = 2;
    public float exitingSpeedY = 5;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = PlayerController.instance;
            player.canMove = false;
            player.GetComponent<VentMovement>().enabled = true;            
        }        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.GetComponent<VentMovement>().enabled = false;
        StartCoroutine(DelayMovementDisable());


        var rb = player.GetComponent<Rigidbody2D>();

        float absX = Math.Abs( rb.velocity.x);
        float absY = Math.Abs( rb.velocity.y);
        if(absX > absY)
        {
            rb.velocity = new Vector2 (exitingSpeedX, 0);
        }
        else
        {
            rb.velocity = new Vector2(0, exitingSpeedY);
        }
    }

    IEnumerator DelayMovementDisable()
    {
        //this line gives the player back its controls, preventing it from being rocketed out.
        yield return new WaitForSeconds(0.01f);
        player.canMove = true;
    }
}
