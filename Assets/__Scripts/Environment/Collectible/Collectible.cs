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

    private float colorTimer = 0f;
    private bool isColorChange = false;
    private bool isSizeChange = false;
    Vector3 origiScale;
    public float beatingDuration = 1;
    [Range(0,2)] public float sizeMulti = 1.02f;


    private void Start()
    {
        origiScale = transform.localScale;
        collider = GetComponentInChildren<Collider2D>();
        caughtEffect = GetComponentInChildren<ParticleSystem>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteObject = GetComponentInChildren<SpriteRenderer>().gameObject;
        origiColor = spriteRenderer.color;
    }

    void Update()
    {
        colorTimer += Time.deltaTime;

        if (colorTimer >= onColorDuration && !isColorChange && !isSizeChange)
        {
            isColorChange = true;
            isSizeChange = true;
            ToggleColor();
            colorTimer = 0f;
        }
    }

    void ToggleColor()
    {
        var origiColor = spriteRenderer.color;

        Sequence colorSequence = DOTween.Sequence();
        colorSequence.Append(spriteRenderer.DOColor(secondColor, onColorDuration / 2).SetEase(Ease.InSine))
            .Append(spriteRenderer.DOColor(origiColor, onColorDuration / 2).SetEase(Ease.InSine))
            .OnComplete(() => isColorChange = false);

        if (!heartBeat) return;


        var newScale = origiScale * sizeMulti;
        Sequence sizeSeq = DOTween.Sequence();
        sizeSeq.Append(transform.DOScale(newScale, beatingDuration / 2).SetEase(Ease.InSine))
            .Append(transform.DOScale(origiScale, beatingDuration / 2).SetEase(Ease.InSine))
            .OnComplete(() => isSizeChange = false);
    }


    //void ToggleColor()
    //{
    //    var origiScale = transform.localScale;
    //    Sequence colorSequence = DOTween.Sequence();
    //    colorSequence.Append(spriteRenderer.DOColor(origiColor, onColorDuration).SetEase(Ease.InSine))
    //        .Append(spriteRenderer.DOColor(secondColor, onColorDuration).SetEase(Ease.InSine))
    //        .Append(spriteRenderer.DOColor(secondColor, onColorDuration).SetEase(Ease.InSine))
    //        .Append(spriteRenderer.DOColor(origiColor, onColorDuration).SetEase(Ease.InSine))
    //        .SetLoops(-1);

    //    if (!heartBeat) return;

    //    var newScale = origiScale * sizeMulti;
    //    Sequence scaleSeq = DOTween.Sequence();
    //    scaleSeq.Append(transform.DOScale(newScale, beatingDuration).SetEase(Ease.InBounce))
    //        .Append(transform.DOScale(origiScale, beatingDuration).SetEase(Ease.OutBounce))
    //        .SetLoops(-1);        
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioManager.Instance.PlayingAudio(AudioManager.Instance.collectible);
        if (collision.CompareTag("Player"))
        {
            var collectibleManager = GameManager.Instance.GetComponent<CollectibleManager>();

            collectibleManager.CollectibleCollected();
            collectibleManager.RegisterSelfAsCollected(gameObject);

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
            //DOTween.Clear();
        }
    }
}
