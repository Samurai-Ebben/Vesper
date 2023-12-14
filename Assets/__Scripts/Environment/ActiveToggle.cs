using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveToggle : MonoBehaviour
{
    public void SetTrue()
    {
        gameObject.SetActive(true);
    }

    public void SetFalse()
    {
        gameObject.SetActive(false);
    }
}
