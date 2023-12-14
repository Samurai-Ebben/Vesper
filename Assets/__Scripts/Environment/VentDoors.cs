using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentDoors : MonoBehaviour
{

    public float suckForce = 200;

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            Vacuum(other);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vacuum(other);
        }
    }

    void Vacuum(Collider2D target)
    {
        var player = PlayerController.instance;
        if (player.currentSize == Sizes.SMALL)
        {
            player.GetComponent<VentMovement>().canMove = false;
            var direction = transform.position - target.transform.position;
            player.rb.velocity = new Vector2(direction.x, direction.y) * suckForce;
            //player.rb.AddForce(direction * suckForce, ForceMode2D.Force);

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        print("Exit");
        if (other.gameObject.CompareTag("Player"))
        {
            print("Exit2");

            var player = PlayerController.instance;
            if (player.currentSize == Sizes.SMALL)
            {
                print("Exit3");

                player.GetComponent<VentMovement>().canMove = true;
            }
        }
    }
}
