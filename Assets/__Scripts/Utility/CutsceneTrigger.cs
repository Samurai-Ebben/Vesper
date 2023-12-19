using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CutsceneTrigger : MonoBehaviour
{
    public Transform cutscenePosition;

    // Objects and their Animator need to start disabled
    public List<GameObject> animationObjects;
    public float animationLength;
    //public string playerAnimationName;
    

    bool cutscenePlayed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !cutscenePlayed)
        {
            PlayerController.instance.isBouncing = true;
            PlayerController.player.transform.position = cutscenePosition.position;
            PlayerController.player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            // TODO lerp/DOTween the player towards cutscenePosition

            PlayAnimations(true);
            cutscenePlayed = true;
            StartCoroutine(ReEnableMovement());
        }
    }

    IEnumerator ReEnableMovement()
    {
        yield return new WaitForSeconds(animationLength);
        PlayerController.instance.isBouncing = false;
        PlayAnimations(false);
    }

    private void PlayAnimations(bool boolean)
    {
        foreach (GameObject animationObject in animationObjects)
        {
            animationObject.SetActive(boolean);
            Animator animator = animationObject.GetComponent<Animator>();

            if (animator != null)
            {
                animator.enabled = boolean;
            }
        }

        Animator playerAnimator = PlayerController.player.GetComponent<Animator>();
        playerAnimator.enabled = boolean;

        //if (boolean)
        //{
        //    playerAnimator.Play(playerAnimationName);
        //}
    }
}
