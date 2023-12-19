using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collectible : MonoBehaviour
{
    ParticleSystem caughtEffect;
    Collider2D collider;
    private void Start()
    {
        collider = GetComponentInChildren<Collider2D>();
        caughtEffect = GetComponentInChildren<ParticleSystem>();
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

}
