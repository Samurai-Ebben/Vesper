using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentDoors : MonoBehaviour
{

    public float suckForce = 5;

    private void OnCollisionStay2D(Collision2D other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            var player = PlayerController.instance;
            if (player.currentSize == Sizes.SMALL)
            {
                //TODO: Suck in the player
                var direction = transform.position - other.transform.position;
                //player.rb.velocity = new Vector2(direction.x * suckForce, direction.y * suckForce);
                player.rb.AddForce(direction * suckForce, ForceMode2D.Impulse);

            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
    }
}
