using System.Collections;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [Header("Push mode")]
    public bool usingRaw;
    public bool usingGravityMultiplier;

    [Header("Values")]
    public float rawBounceForce = 50;
    public float GravityScaleMultiplier = 5;
    public float bounceDelay = 0.1f;
    
    [HideInInspector]
    public float bounceForce;
    
    PlayerController player;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<PlayerController>();
            var rb2d = player.GetComponent<Rigidbody2D>();

            //What is this for??
            if (usingGravityMultiplier)
            {
                bounceForce = rb2d.gravityScale * GravityScaleMultiplier;
                usingRaw = false;
            }

            else
            {
                bounceForce = rawBounceForce;
                usingRaw = true;
            }

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
