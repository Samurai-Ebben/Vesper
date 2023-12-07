using UnityEngine;
using System.Collections.Generic;

public class ResettableObjectManager : MonoBehaviour
{
    public static ResettableObjectManager Instance;

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

    public void UnregisterObject(IReset resetObject)
    {
        resettableObjects.Remove(resetObject);
    }

    public void ResetAllObjects()
    {
        //print(reset)
        foreach (IReset resetObject in resettableObjects)
        {
            resetObject.Reset();
        }
    }
}
