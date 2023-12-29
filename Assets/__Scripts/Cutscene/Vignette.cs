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

    void Start()
    {
        if (cam == null)
        {
            cam = GetComponentInParent<Camera>();
        }

        originalTargetScaleY = targetScaleY;
        originalScale = transform.localScale;
        originalCameraSize = cam.orthographicSize;
    }

    void Update()
    {
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
        Vector3 targetScale = new Vector3(originalScale.x, targetScaleY, 0);
        transform.DOScale(targetScale, expandDuration);
    }
}
