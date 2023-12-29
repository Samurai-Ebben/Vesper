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
    private int numberOfOutlines;
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

    public float animationDuration;
    public List<GameObject> animationObjects;

    FadeSprite fadeSprite;
    Rigidbody2D rb2d;
    bool cutscenePlayed = false;

    void Start()
    {
        fadeSprite = GetComponent<FadeSprite>();
        rb2d = PlayerController.player.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !cutscenePlayed)
        {
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
            Instantiate(outline);
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
