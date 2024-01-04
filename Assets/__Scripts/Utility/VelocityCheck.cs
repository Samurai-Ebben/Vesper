using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VelocityCheck : MonoBehaviour
{
    public float breakForce;
    public UnityEvent DisableSelf;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponentInParent<PlayerController>();
            RayCastHandler rayCastHandler = other.GetComponentInParent<RayCastHandler>();

            if (playerController.GetMagnitude() < breakForce) return;
            if (playerController.currentSize != Sizes.BIG) return;
            //if (playerController.GetComponent<RayCastHandler>().checkAllToGround) return;
            //if (!rayCastHandler.fullyOnPlatform) return;

            DisableSelf.Invoke();
            return;
        }

      
    }
}
