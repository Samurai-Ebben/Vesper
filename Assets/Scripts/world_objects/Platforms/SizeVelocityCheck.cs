using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SizeVelocityCheck : MonoBehaviour
{
    public float velocityRequirement;
    public UnityEvent sampleEvent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D rb2d = other.GetComponent<Rigidbody2D>();
            PlayerController playerController = other.GetComponent<PlayerController>();

            print("player velocity.magnitude: " + rb2d.velocity.magnitude);

            if (rb2d.velocity.magnitude >= velocityRequirement && playerController.isLarge)
            {
                sampleEvent.Invoke();
            }
        }
    }
}
