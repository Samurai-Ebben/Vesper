using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappearing : MonoBehaviour
{
    public float cooldown = 0.5f;
    public float sustainTime = 1f;

    public GameObject platform;

    DetectionPlayerCollision detectionPlayerCollision;

    void Start()
    {
        detectionPlayerCollision = GetComponentInChildren<DetectionPlayerCollision>();
    }

    public void Disappear()
    {
        StartCoroutine(DisappearAndComeBack());
    }

    IEnumerator DisappearAndComeBack()
    {
        yield return new WaitForSeconds(sustainTime);

        platform.SetActive(false);

        yield return new WaitForSeconds(cooldown);

        platform.SetActive(true);
        detectionPlayerCollision.SetDefaultColor();
    }
}
