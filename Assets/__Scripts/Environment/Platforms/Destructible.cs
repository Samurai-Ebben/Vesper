using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour, IReset
{
    public List<GameObject> terrainObject;
    ScreenShakeHandler screenShakeHandler;

    bool ongoingCoroutine;

    [Header("Respawn")]
    public bool respawnEnabled = false;
    public float respawnDelay = 5f;

    public void TriggerDestroy()
    {
        screenShakeHandler = Camera.main.GetComponent<ScreenShakeHandler>();
        if (!ongoingCoroutine)
        {
            screenShakeHandler.DestructionShake();
            StartCoroutine(Destroy());
        }
    }

    IEnumerator Destroy()
    {
        ongoingCoroutine = true;

        foreach (GameObject obj in terrainObject)
        {
            obj.SetActive(false);
        }

        if (respawnEnabled)
        {
            yield return new WaitForSeconds(respawnDelay);
            foreach (GameObject obj in terrainObject)
            {
                obj.SetActive(true);
            }
        }

        ongoingCoroutine = false;
    }

    public void Reset()
    {
        foreach (GameObject obj in terrainObject)
        {
            obj.SetActive(true);
        }
    }
    private void OnEnable()
    {
        ResettableObjectManager.Instance.RegisterObject(this);
    }

    private void OnDisable()
    {
        ResettableObjectManager.Instance.UnregisterObject(this);
    }
}
