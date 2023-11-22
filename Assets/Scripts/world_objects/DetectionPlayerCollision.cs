using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DetectionPlayerCollision : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Color32 defaultColor;

    Disappearing disappearing;

    void Start()
    {
        disappearing = GetComponentInParent<Disappearing>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        spriteRenderer.color = Color.red;
        disappearing.Disappear();
    }

    public void SetDefaultColor()
    {
        spriteRenderer.color = defaultColor;
    }

}
