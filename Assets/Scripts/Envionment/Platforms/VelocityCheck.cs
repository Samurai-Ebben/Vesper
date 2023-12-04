using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VelocityCheck : MonoBehaviour
{
    public float breakForce;
    public UnityEvent sampleEvent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();

            if (playerController.GetMagnitude() < breakForce) return;
            if (playerController.currentSize != Sizes.LARGE) return;

            sampleEvent.Invoke();
            return;
        }
    }
}
