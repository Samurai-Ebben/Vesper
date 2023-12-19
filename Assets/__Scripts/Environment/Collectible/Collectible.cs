using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collectible : MonoBehaviour
{
    ParticleSystem caughtEffect;
    Collider2D collider;
    SpriteRenderer spriteRenderer;
    Color origiColor;
    public Color secondColor;

    public float duration = 1;
    private void Start()
    {
        collider = GetComponentInChildren<Collider2D>();
        caughtEffect = GetComponentInChildren<ParticleSystem>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        origiColor = spriteRenderer.color;
        ToggleColor();
    }

    void ToggleColor()
    {
        Sequence colorSequence = DOTween.Sequence();
        colorSequence.Append(spriteRenderer.DOColor(origiColor, duration).SetEase(Ease.Linear))
            .AppendInterval(0.5f) // Change duration between color transitions
            .Append(spriteRenderer.DOColor(secondColor, duration).SetEase(Ease.Linear))
            .AppendInterval(0.5f) // Change duration between color transitions
            .SetLoops(-1);
        colorSequence.Append(spriteRenderer.DOColor(secondColor, duration).SetEase(Ease.Linear))
            .AppendInterval(0.5f) // Change duration between color transitions
            .Append(spriteRenderer.DOColor(origiColor, duration).SetEase(Ease.Linear))
            .AppendInterval(0.5f) // Change duration between color transitions
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
            Taken();
        }
    }

    void Taken()
    {
        var sprite = gameObject.GetComponentInChildren<SpriteRenderer>().gameObject;
        if (sprite != null)
        {
            sprite.SetActive(false);
            collider.enabled = false;
        }
    }

    private void OnDestroy()
    {
        DOTween.Clear(transform);
    }

}
