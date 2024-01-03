using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Diagnostics;
using UnityEngine.SceneManagement;

public class Collectible : MonoBehaviour
{
    Collider2D collider;
    SpriteRenderer spriteRenderer;
    ParticleSystem[] particleSystems;
    OutlineFxTrigger outlineFxTrigger;
    GameObject spriteObject;

    Color origiColor;
    public Color secondColor;
    [Range(0, .9f)] public float delayBetweenColors = 0.1f;
    public float onColorDuration = 1;
    public bool heartBeat = true;

    private float colorTimer = 0f;
    private bool isColorChange = false;
    private bool isSizeChange = false;
    public float beatingDuration = 1;
    Vector3 origiScale;
    [Range(0,2)] public float sizeMulti = 1.02f;

    private void Start()
    {
        origiScale = transform.localScale;
        collider = GetComponentInChildren<Collider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        particleSystems = GetComponentsInChildren<ParticleSystem>();
        outlineFxTrigger = GetComponentInChildren<OutlineFxTrigger>();
        spriteObject = spriteRenderer.gameObject;
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var audioManager = AudioManager.Instance;
            audioManager.GameplaySFX(audioManager.collectible, audioManager.collectibleVolume);
            
            var collectibleManager = GameManager.Instance.GetComponent<CollectibleManager>();
            collectibleManager.CollectibleCollected();
            collectibleManager.RegisterSelfAsCollected(gameObject);

            outlineFxTrigger.PlayFx();

            foreach (ParticleSystem particle in particleSystems)
            {
                particle.Play();
            }

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
