using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Diagnostics;

public class Collectible : MonoBehaviour
{
    ParticleSystem caughtEffect;
    Collider2D collider;
    SpriteRenderer spriteRenderer;
    GameObject spriteObject;
    Color origiColor;
    public Color secondColor;

    public float blinkingDuration = 1;
    public bool heartBeat = true;

    public float beatingDuration = 1;
    [Range(0,2)] public float sizeMulti = 1.02f;
    private void Start()
    {
        collider = GetComponentInChildren<Collider2D>();
        caughtEffect = GetComponentInChildren<ParticleSystem>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteObject = gameObject.GetComponentInChildren<SpriteRenderer>().gameObject;
        origiColor = spriteRenderer.color;
        ToggleColor();
    }

    void ToggleColor()
    {
        var origiScale = transform.localScale;
        Sequence colorSequence = DOTween.Sequence();
        colorSequence.Append(spriteRenderer.DOColor(origiColor, blinkingDuration).SetEase(Ease.Linear))
            .AppendInterval(0.1f)
            .Append(spriteRenderer.DOColor(secondColor, blinkingDuration).SetEase(Ease.Linear))
            .AppendInterval(0.1f)
            .SetLoops(-1);
        colorSequence.Append(spriteRenderer.DOColor(secondColor, blinkingDuration).SetEase(Ease.Linear))
            .AppendInterval(0) 
            .Append(spriteRenderer.DOColor(origiColor, blinkingDuration).SetEase(Ease.Linear))
            .AppendInterval(0)
            .SetLoops(-1);

        if (!heartBeat) return;

        var newScale = origiScale * sizeMulti;
        Sequence scaleSeq = DOTween.Sequence();
        scaleSeq.Append(transform.DOScale(newScale, beatingDuration).SetEase(Ease.InBounce))
            .AppendInterval(beatingDuration / 2)
            .Append(transform.DOScale(origiScale, beatingDuration).SetEase(Ease.OutBounce))
            .AppendInterval(beatingDuration / 2)
            .SetLoops(-1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var collectibleManager = GameManager.Instance.GetComponent<CollectibleManager>();

            collectibleManager.CollectibleCollected();
            collectibleManager.RegisterSelfAsCollected(gameObject);

            // TODO Animation/Particles
            caughtEffect.Play();
            ToggleActive(false);
        }
    }

    public void ToggleActive(bool boolean)
    {
        if (spriteObject != null)
        {
            spriteObject.SetActive(boolean);
            collider.enabled = boolean;
        }
    }

    private void OnDestroy()
    {
        DOTween.Clear(transform);
    }

}
