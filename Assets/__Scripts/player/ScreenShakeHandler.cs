using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeHandler : MonoBehaviour, IReset
{
    [Header("Duration")]
    public float destructionDuration = 0.04f;
    public float jumpDuration = 0.07f;
    public float deathDuration = 0.04f;
    public float platformsDuration = 0.04f;
    public float cornerDuration = 0.01f;

    [Header("Strength")]
    public float strengthForDestruction = 0.1f;
    public float strengthForJump = 0.01f;
    public float strengthForDeath = 0.04f;
    public float strengthForPlatforms = 0.04f;
    public float strengthCorner = 0.01f;

    public bool vertical;
    public bool horizontal;

    Vector3 origPos;

    Vector3 RandomPosition;

    void Start()
    {
        RegisterSelfToResettableManager();
        origPos = transform.position;
    }

    public void CornerShake()
    {
        vertical = false;
        horizontal = false;
        StartCoroutine(ShakeScreen(strengthCorner, cornerDuration));
    }
    public void JumpShake()
    {
        vertical = true;
        PlayerController.instance.VibrateController(strengthForJump * 10, 1f, jumpDuration * 10);
        StartCoroutine(ShakeScreen(strengthForJump, jumpDuration));
    }

    public void DestructionShake()
    {
        vertical = true;
        PlayerController.instance.VibrateController(.5f, 1f, destructionDuration);

        StartCoroutine(ShakeScreen(strengthForDestruction, destructionDuration));
    }
    public void DeathShake()
    {
        vertical = false;
        horizontal = false;
        PlayerController.instance.VibrateController(.4f, .55f, .1f);

        StartCoroutine(ShakeScreen(strengthForDeath, deathDuration));
    }

    public void PlatformShakeOnTarget()
    {
        vertical = true;
        horizontal = true;
        PlayerController.instance.VibrateController(.25f, .55f, platformsDuration);
        StartCoroutine(ShakeScreen(strengthForPlatforms, platformsDuration));
    }

    IEnumerator ShakeScreen(float strength, float duration)
    {
        if (vertical)
        {
            horizontal = false;
            RandomPosition.y = Random.Range(0, 10);
        }

        if (horizontal)
        {
            vertical = false;
            RandomPosition.x = Random.Range(0, 10);
        }

        if (!horizontal && !vertical)
        {
            RandomPosition = Random.insideUnitCircle;
        }
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;

            transform.position = transform.position + RandomPosition * strength;
            yield return null;
        }

        transform.position = origPos;


    }

    public void Reset()
    {
        vertical = true;
    }
    public void RegisterSelfToResettableManager()
    {
        ResettableManager.Instance.RegisterObject(this);
    }
}
