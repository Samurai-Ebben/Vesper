using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquishAndSquashToggle : MonoBehaviour
{
    SquishAndSquash squishAndSquash;

    void Start()
    {
        
    }

    public void ToggleSquishAndSquash(bool boolean)
    {
        squishAndSquash = PlayerController.instance.GetComponentInChildren<SquishAndSquash>();
        squishAndSquash.ToggleEnabled(boolean);
    }
}
