using System.Collections;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public bool pushWithBounceForce;
    public float bounceForce = 200;
    public float bounceDelay = 0.1f;
    
    PlayerController player;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<PlayerController>();
            var rb2d = player.GetComponent<Rigidbody2D>();

            //What is this for??
            //if (!pushWithBounceForce)
            //{
            //    bounceForce = rb2d.gravityScale * 5;
            //}

            if (rb2d != null)
            {
                player.isBouncing = true;
                Vector2 bounceDirection = transform.up * bounceForce;
                rb2d.AddForce(bounceDirection, ForceMode2D.Impulse);

                StartCoroutine(EnableMovement());
            }
        }
    }

    IEnumerator EnableMovement()
    {
        yield return new WaitForSeconds(bounceDelay);
        player.isBouncing = false;
    }

}
