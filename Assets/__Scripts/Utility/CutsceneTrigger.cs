using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CutsceneTrigger : MonoBehaviour
{
    // Objects and their Animator needs to start disabled
    public List<GameObject> animationObjects;
    public Transform cutscenePosition;
    public float animationLength = 4;

    bool cutscenePlayed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !cutscenePlayed)
        {
            PlayerController.instance.isBouncing = true;
            PlayerController.player.transform.position = cutscenePosition.position;
            PlayerController.player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            // TODO lerp/DOTween the player towards cutscenePositiono

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
    }
}
