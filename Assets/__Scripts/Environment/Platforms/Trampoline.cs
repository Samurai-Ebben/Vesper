using DG.Tweening.Core.Easing;
using System.Collections;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [Header("Push mode")]
    public bool usingGravityMultiplier;
    public bool usingVelocityMultiplier;

    [Header("Values")]
    public float gravityScaleMultiplier = 5;
    public float velocityMultiplier = 0.1f;
    public float maxBounceForce = 30;
    public float bounceDelay = 0.1f;
    
    [HideInInspector]
    public float bounceForce;
    
    PlayerController player;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject.GetComponentInParent<PlayerController>();
            var rb2d = player.GetComponent<Rigidbody2D>();

            if (usingGravityMultiplier)
            {
                bounceForce += rb2d.gravityScale * gravityScaleMultiplier;
            }

            if (usingVelocityMultiplier)
            {
                bounceForce += player.GetMagnitude() * velocityMultiplier;
            }

            if (bounceForce > maxBounceForce)
            {
                bounceForce = maxBounceForce;
            }

            if (rb2d != null)
            {
                player.isBouncing = true;
                Vector2 bounceDirection = transform.up * bounceForce;
                rb2d.AddForce(bounceDirection, ForceMode2D.Impulse);

                StartCoroutine(EnableMovement());
            }

            bounceForce = 0;
        }
    }

    IEnumerator EnableMovement()
    {
        yield return new WaitForSeconds(bounceDelay);
        player.isBouncing = false;
    }

}
