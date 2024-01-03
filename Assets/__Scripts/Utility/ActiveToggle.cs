using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveToggle : MonoBehaviour
{
    public List<GameObject> targetGameObjects;

    public void SetTrue()
    {
        foreach (GameObject target in targetGameObjects)
        {
            target.SetActive(true);
        }
    }

    public void SetFalse()
    {
        foreach (GameObject target in targetGameObjects)
        {
            target.SetActive(false);
        }
    }
}
