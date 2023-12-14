using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FadeSprite : MonoBehaviour
{
    public UnityEvent callAfterFadeOut;
    public SpriteRenderer spriteRenderer;
    public float fadeInTime = 1.5f;
    public float fadeOutTime = 1.5f;
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
        StopCoroutine(CallDisable());

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

    public void DisableAfterFadeOut()
    {
        StartCoroutine(CallDisable());
    }

    IEnumerator CallDisable()
    {
        yield return new WaitForSeconds(fadeOutTime);
        callAfterFadeOut.Invoke();
    }
}
