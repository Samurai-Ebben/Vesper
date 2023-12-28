using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using DG.Tweening;

public class CutsceneTrigger : MonoBehaviour
{
    public Transform targetPosition;
    public float moveDuration;

    // Objects and their Animator need to start disabled
    public List<GameObject> animationObjects;
    public float animationLength;

    Rigidbody2D rb2d;
    bool cutscenePlayed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !cutscenePlayed)
        {
            rb2d = PlayerController.player.GetComponent<Rigidbody2D>();
            
            StopPlayerMovement();
            MovePlayerToCutscenePosition();
            PlayAnimations(true);
            StartCoroutine(UnstopPlayerMovement());

            cutscenePlayed = true;
        }
    }

    private void StopPlayerMovement()
    {
        PlayerController.instance.isBouncing = true;
        rb2d.velocity = Vector2.zero;
    }

    IEnumerator UnstopPlayerMovement()
    {
        yield return new WaitForSeconds(animationLength);
        PlayerController.instance.isBouncing = false;
        PlayAnimations(false);
    }

    private void PlayAnimations(bool boolean)
    {
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
        
        // Enable Player Animator
        Animator playerAnimator = PlayerController.player.GetComponent<Animator>();
        playerAnimator.enabled = boolean;
    }

    void MovePlayerToCutscenePosition()
    {
        rb2d.transform.DOMove(targetPosition.position, moveDuration);
    }
}
