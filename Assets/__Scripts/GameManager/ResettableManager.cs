using UnityEngine;
using System.Collections.Generic;

public class ResettableManager : MonoBehaviour
{
    public static ResettableManager Instance;

    private List<IReset> resettableObjects = new List<IReset>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterObject(IReset resetObject)
    {
        resettableObjects.Add(resetObject);
    }

    public void ResetAllObjects()
    {
        foreach (IReset resetObject in resettableObjects)
        {
            resetObject.Reset();
        }
    }
}
