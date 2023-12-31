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
            AudioManager.Instance.GameplaySFX(AudioManager.Instance.risingPlatformSound, AudioManager.Instance.risingPlatformVolume);
            PlayerController.instance.screenShake.PlatformShakeOnTarget();
            if (risingMovement.rocks == null) return;
            risingMovement.rocks.Play();
        }
    }
}
