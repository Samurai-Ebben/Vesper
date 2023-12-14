using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Destructible : MonoBehaviour, IReset
{
    public List<GameObject> terrainObject;
    public ParticleSystem particles;
    ScreenShakeHandler screenShakeHandler;

    bool ongoingCoroutine;

    [Header("Respawn")]
    public bool respawnEnabled = false;
    public float respawnDelay = 5f;

    private void Start()
    {
        RegisterSelfToResettableManager();
    }

    public void TriggerDestroy()
    {
        screenShakeHandler = Camera.main.GetComponent<ScreenShakeHandler>();
        if (!ongoingCoroutine)
        {
            particles.Play();
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
    public void RegisterSelfToResettableManager()
    {
        ResettableManager.Instance.RegisterObject(this);
    }
}
