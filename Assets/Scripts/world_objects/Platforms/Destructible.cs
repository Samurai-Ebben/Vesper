using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public GameObject terrainObject;
    SpriteRenderer objectSpriteRenderer;

    bool ongoingCoroutine;

    public Color32 onTriggerColor = Color.red;
    private Color32 defaultColor;

    [Header("Respawn")]
    public bool respawnEnabled = false;
    public float respawnDelay = 5f;


    void Start()
    {
        objectSpriteRenderer = terrainObject.GetComponent<SpriteRenderer>();
        defaultColor = objectSpriteRenderer.color;
    }

    public void GetDestroyed()
    {
        if (!ongoingCoroutine)
        {
            StartCoroutine(Demolish());
        }
    }

    IEnumerator Demolish()
    {
        ongoingCoroutine = true;

        objectSpriteRenderer.color = onTriggerColor;

        terrainObject.SetActive(false);

        if (respawnEnabled)
        {
            yield return new WaitForSeconds(respawnDelay);
            terrainObject.SetActive(true);
        }

        objectSpriteRenderer.color = defaultColor;

        ongoingCoroutine = false;
    }
}
