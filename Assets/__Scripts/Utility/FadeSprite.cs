using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FadeSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float fadeInTime = 1.5f;
    public float fadeOutTime = 1.5f;
    public UnityEvent actionAfterFadeOut;
    float fadeTime;

    private float targetAlpha;
    private float timer = 0f;
    private Color originalColor;
    private bool isFading = false;

    private void Start()
    {
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    private void Update()
    {
        Fade();
    }

    private void Fade()
    {
        if (spriteRenderer == null || !isFading)
        {
            return;
        }

        timer += Time.deltaTime;

        float normalizedTime = Mathf.Clamp01(timer / fadeTime);
        Color targetColor = originalColor;
        targetColor.a = Mathf.Lerp(originalColor.a, targetAlpha, normalizedTime);
        spriteRenderer.color = targetColor;

        if (normalizedTime >= 1f)
        {
            isFading = false;
        }
    }

    public void FadeIn()
    {
        StopCoroutine(AfterFadeOut());

        originalColor.a = 0;
        timer = 0f;
        isFading = true;
        targetAlpha = 1;
        fadeTime = fadeInTime;
    }
    
    public void FadeOut()
    {
        originalColor.a = 1;
        timer = 0f;
        isFading = true;
        targetAlpha = 0f;
        fadeTime = fadeOutTime;
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
