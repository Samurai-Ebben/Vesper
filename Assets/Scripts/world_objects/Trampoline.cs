using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float bounceForce = 10f;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb2d = other.gameObject.GetComponent<Rigidbody2D>();

            if (rb2d != null)
            {
                
                bounceForce = rb2d.gravityScale * 5;
                Vector2 bounceDirection = new Vector2(0f, bounceForce);
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                rb2d.AddForce(bounceDirection, ForceMode2D.Impulse);

                //PlayerController player = rb2d.GetComponent<PlayerController>();
                //rb2d.velocity= Vector2.up*bounceForce;
            }
        }
    }
}
