using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingShake : MonoBehaviour
{
    RisingMovement risingMovement;
    private void Awake()
    {
        risingMovement = transform.parent.GetComponent<RisingMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Rising"))
        {
            risingMovement.rocks.Play();
            PlayerController.instance.screenShake.PlatformShakeOnTarget();
        }
    }
}
