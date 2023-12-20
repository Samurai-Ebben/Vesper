using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;
using Unity.VisualScripting;

public class FadeSpriteRandom : MonoBehaviour
{
    public bool fadeActive;

    [Range(0, 1)] 
    public float fadedAlpha;
    private float fullAlpha = 1;

    public float fadeOutTime;
    public float fadeInTime;

    public float minTimeBetweenFade;
    public float maxTimeBetweenFade;

    SpriteRenderer spriteRenderer;

    void OnEnable()
    {
        fadeActive = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(RandomFade());
    }

    IEnumerator RandomFade()
    {
        yield return new WaitForSeconds(Random.Range(minTimeBetweenFade, maxTimeBetweenFade));
        
        FadeOut();

        yield return new WaitForSeconds(fadeOutTime); 
        
        FadeIn();

        yield return new WaitForSeconds(fadeInTime);

        StartCoroutine(RandomFade());
    }

    public void ToggleActiveFade()
    {
        fadeActive = !fadeActive;
    }
    void FadeOut()
    {
        if (fadeActive)
        {
            spriteRenderer.DOFade(fadedAlpha, fadeOutTime);
        }
    }
    void FadeIn()
    {
        if (fadeActive)
        {
            spriteRenderer.DOFade(fullAlpha, fadeInTime);
        }
    }
}
