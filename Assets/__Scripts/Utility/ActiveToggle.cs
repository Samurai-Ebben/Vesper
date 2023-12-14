using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveToggle : MonoBehaviour
{
    public GameObject targetGameObject;
    public void SetTrue()
    {
        targetGameObject.SetActive(true);
    }

    public void SetFalse()
    {
        targetGameObject.SetActive(false);
    }
}
