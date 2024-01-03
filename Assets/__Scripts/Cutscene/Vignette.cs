using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vignette : MonoBehaviour
{
    public bool topVignette;
    public float expandDuration;
    public float targetScaleY;
    public Camera cam;

    private float originalTargetScaleY;
    private float originalCameraSize;
    private Vector3 originalScale;

    float timer;
    void Start()
    {
        if (cam == null)        
            cam = GetComponentInParent<Camera>();
        if (cam == null)
            cam = Camera.main;

        originalScale = transform.localScale;
        originalTargetScaleY = targetScaleY;
        originalCameraSize = cam.orthographicSize;
    }

    void Update()
    {
        timer += Time.deltaTime;

        MatchCameraSize();
        Expand();
    }

    void MatchCameraSize()
    {
        float camExpandRatio = cam.orthographicSize / originalCameraSize;

        targetScaleY = originalTargetScaleY * camExpandRatio;
        transform.localScale = originalScale * camExpandRatio;

        if (topVignette)
        {
            transform.position = cam.ViewportToWorldPoint(new Vector3(0.5f, 1, cam.nearClipPlane +1));
        }
        else
        {
            transform.position = cam.ViewportToWorldPoint(new Vector3(0.5f, 0, cam.nearClipPlane + 1));
        }
    }

    void Expand()
    {
        //Vector3 targetScale = new Vector3(originalScale.x, targetScaleY, 0);
        //transform.DOScale(targetScale, expandDuration);

        float lerpT = Mathf.Clamp01(timer / expandDuration);
        float currentScaleY = Mathf.Lerp(originalScale.y, targetScaleY, lerpT);

        transform.localScale = new Vector3(originalScale.x, currentScaleY, 0);
    }
}
