using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour, IReset
{
    public List<GameObject> terrainObject;

    bool ongoingCoroutine;

    [Header("Respawn")]
    public bool respawnEnabled = false;
    public float respawnDelay = 5f;

    public void TriggerDestroy()
    {
        if (!ongoingCoroutine)
        {
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

    public void ResetPlatform()
    {
        foreach (GameObject obj in terrainObject)
        {
            obj.SetActive(true);
        }
    }
}
