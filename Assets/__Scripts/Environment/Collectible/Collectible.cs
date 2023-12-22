using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Diagnostics;
using UnityEngine.SceneManagement;

public class Collectible : MonoBehaviour
{
    ParticleSystem caughtEffect;
    Collider2D collider;
    SpriteRenderer spriteRenderer;
    GameObject spriteObject;
    Color origiColor;
    public Color secondColor;
    [Range(0,.9f)]public float delayBetweenColors = 0.1f;
    public float onColorDuration = 1;
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
        colorSequence.Append(spriteRenderer.DOColor(origiColor, onColorDuration).SetEase(Ease.Linear))
            .AppendInterval(delayBetweenColors)
            .Append(spriteRenderer.DOColor(secondColor, onColorDuration).SetEase(Ease.Linear))
            .AppendInterval(delayBetweenColors)
            .SetLoops(-1);
        colorSequence.Append(spriteRenderer.DOColor(secondColor, onColorDuration).SetEase(Ease.Linear))
            .AppendInterval(delayBetweenColors) 
            .Append(spriteRenderer.DOColor(origiColor, onColorDuration).SetEase(Ease.Linear))
            .AppendInterval(delayBetweenColors)
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

    //private void OnDestroy()
    //{
    //    DOTween.Clear(transform);
    //}

}
