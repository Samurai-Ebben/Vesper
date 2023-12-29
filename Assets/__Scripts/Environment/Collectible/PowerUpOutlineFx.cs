using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PowerUpOutlineFx : MonoBehaviour//, IReset
{
    public float growDuration;
    public float targetScale;
    public float fadeDelay;
    public Ease ease;
    bool playing;

    //private Vector3 originalScale;

    private void Start()
    {
        TriggerEffect();

        //originalScale = transform.localScale;
    }
    private void Update()
    {
        if (!playing)
        {
            TriggerEffect();
        }
    }

    void TriggerEffect()
    {
        playing = true;
        transform.DOScale(targetScale, growDuration).SetEase(ease);
        StartCoroutine(TriggerFade());
        //StartCoroutine(DELETEME());
    }

    IEnumerator TriggerFade()
    {
        yield return new WaitForSeconds(fadeDelay);
        GetComponent<FadeSprite>().FadeOut();
    }

    //IEnumerator DELETEME()
    //{
    //    yield return new WaitForSeconds(growDuration);
    //    playing = false;
    //    Reset();
    //}

    //public void Reset()
    //{
    //    transform.DOScale(originalScale, 0);
    //}

    //public void RegisterSelfToResettableManager()
    //{
    //    ResettableManager.Instance.RegisterObject(this);
    //}
}
