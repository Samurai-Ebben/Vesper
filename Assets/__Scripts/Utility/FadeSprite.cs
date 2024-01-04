using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class FadeSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float fadeInTime = 1.5f;
    public float fadeOutTime = 1.5f;
    public UnityEvent actionAfterFadeOut;

    float fadeTime;
    float targetAlpha;
    Color startColor;

    private void Start()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        if (spriteRenderer != null)
        {
            startColor = spriteRenderer.color;
        }
    }
    
    public void FadeOut()
    {
        //print("Fade out triggered");

        fadeTime = fadeOutTime;
        targetAlpha = 0f;

        startColor.a = 1;
        spriteRenderer.color = startColor;

        Fade();
    }

    public void FadeIn()
    {
        //print("Fade in triggered");
        StopCoroutine(AfterFadeOut());

        fadeTime = fadeInTime;
        targetAlpha = 1;
        
        startColor.a = 0;
        spriteRenderer.color = startColor;

        Fade();
    }

    private void Fade()
    {
        spriteRenderer.DOFade(targetAlpha, fadeTime);
    }

    public void CallAfterFadeOut()
    {
        StartCoroutine(AfterFadeOut());
    }

    IEnumerator AfterFadeOut()
    {
        yield return new WaitForSeconds(fadeOutTime);
        actionAfterFadeOut.Invoke();
    }
}
