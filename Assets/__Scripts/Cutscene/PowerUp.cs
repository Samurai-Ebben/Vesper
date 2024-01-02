using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PowerUp : MonoBehaviour
{
    [Header("Power-Up")]
    public bool enableLargeSize;
    public bool enableSmallSize;

    [Header("Sprite effects")]
    public bool spriteFade = true;
    public bool outlineFx = true;
    public float outlineSpawnDelay;
    public int numberOfOutlines;
    public GameObject outline;


    [Header("Movement")]
    public bool stopPlayerMovement = true;
    public bool movePlayerToPosition = true;

    public Transform targetPosition;
    public float moveDuration;

    // Objects & Animator needs to play on awake
    [Header("Animation")]
    public bool playPlayerAnimation = true;
    public bool playObjectsAnimation = true;
    public float beatingDuration = 1;
    [Range(0, 2)] public float sizeMulti = 1.02f;
    float pulsDuration = 1;
    float pulsingTimer = 0;
    public float animationDuration;
    public List<GameObject> animationObjects;

    FadeSprite fadeSprite;
    Rigidbody2D rb2d;
    bool cutscenePlayed = false;
    Vector3 origiScale;
    private bool isSizeChange = false;

    void Start()
    {
        origiScale = transform.localScale;
        fadeSprite = GetComponent<FadeSprite>();
        rb2d = PlayerController.player.GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        pulsingTimer += Time.deltaTime;

        if (pulsingTimer >= pulsDuration && !isSizeChange)
        {
            isSizeChange = true;
            Pulsing ();
            pulsingTimer = 0f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !cutscenePlayed)
        {
            Debug.Log("Ljud");
            AudioManager.Instance.GameplaySFX(AudioManager.Instance.powerUpSound, AudioManager.Instance.powerUpVolume);
            SpriteFade();
            OutlineFx();
            StopPlayerMovement();
            MovePlayerToCutscenePosition();
            PlayObjectsAnimations(true);
            PlayPlayerAnimation(true);

            cutscenePlayed = true;

            StartCoroutine(UnstopMovementAndStopAnimations());
        }
    }

    void SpriteFade()
    {
        if (!spriteFade) return;
        fadeSprite.FadeOut();
    }

    void OutlineFx()
    {
        if (!outlineFx) return;
        StartCoroutine(TriggerOutlineFx());
    }
    IEnumerator TriggerOutlineFx()
    {
        for (int i = 0; i < numberOfOutlines; i++)
        {
            Instantiate(outline, transform.position, Quaternion.identity);            
            yield return new WaitForSeconds(outlineSpawnDelay);
        }
    }

    void StopPlayerMovement()
    {
        if (!stopPlayerMovement) return;

        rb2d.velocity = Vector2.zero;
        PlayerController.instance.canMove = false;

        PlayerController.instance.bigEnabled = false;
        PlayerController.instance.smallEnabled = false;
    }
    IEnumerator UnstopMovementAndStopAnimations()
    {
        yield return new WaitForSeconds(animationDuration);
        PlayerController.instance.canMove = true;
        PlayObjectsAnimations(false);
        PlayPlayerAnimation(false);

        EnableSize();
    }
    public void Pulsing()
    {
        var newScale = origiScale * sizeMulti;
        Sequence sizeSeq = DOTween.Sequence();
        sizeSeq.Append(transform.DOScale(newScale, beatingDuration / 2).SetEase(Ease.InSine))
            .Append(transform.DOScale(origiScale, beatingDuration / 2).SetEase(Ease.InSine))
            .OnComplete(() => isSizeChange = false);
    }

    private void EnableSize()
    {
        PlayerController.instance.bigEnabled = enableLargeSize;
        PlayerController.instance.smallEnabled = enableSmallSize;
    }

    void MovePlayerToCutscenePosition()
    {
        if (!movePlayerToPosition) return;

        rb2d.transform.DOMove(targetPosition.position, moveDuration);
    }

    void PlayObjectsAnimations(bool boolean)
    {
        if (!playObjectsAnimation) return;

        // Enable animationObjects and their Animator
        foreach (GameObject animationObject in animationObjects)
        {
            animationObject.SetActive(boolean);
            Animator animator = animationObject.GetComponent<Animator>();

            if (animator != null)
            {
                animator.enabled = boolean;
            }
        }
    }

    void PlayPlayerAnimation(bool boolean)
    {
        if (!playPlayerAnimation) return;

        // Enable Player Animator
        Animator playerAnimator = PlayerController.player.GetComponent<Animator>();
        playerAnimator.enabled = boolean;
    }
}
