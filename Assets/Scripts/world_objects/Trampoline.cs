using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public bool pushWithBounceForce;
    public float bounceForce = 10f;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponent<PlayerController>();
            var rb2d = player.GetComponent<Rigidbody2D>();
            
            if (!pushWithBounceForce)
            {
                bounceForce = rb2d.gravityScale * 5;
            }

            if (rb2d != null)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0);

                Vector2 bounceDirection = transform.up * bounceForce;
                rb2d.AddForce(bounceDirection, ForceMode2D.Impulse);
            }
        }
    }
}
