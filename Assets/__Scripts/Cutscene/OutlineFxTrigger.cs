using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutlineFxTrigger : MonoBehaviour
{
    public float outlineSpawnDelay = 0.3f;
    public int numberOfOutlines = 1;
    public GameObject outline;

    public void PlayFx()
    {
        StartCoroutine(TriggerOutlineFx());
    }
    IEnumerator TriggerOutlineFx()
    {
        for (int i = 0; i < numberOfOutlines; i++)
        {
            Instantiate(outline, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(outlineSpawnDelay);
        }
    }
}
