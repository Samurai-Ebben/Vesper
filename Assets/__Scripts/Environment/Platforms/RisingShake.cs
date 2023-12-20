using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingShake : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Rising"))
        {
            PlayerController.instance.screenShake.PlatformShakeOnTarget();
        }
    }
}
