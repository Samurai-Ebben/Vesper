using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CutsceneTrigger : MonoBehaviour
{
    public GameObject particles;
    public Animator particlesAnimator;
    public Transform cutscenePosition;
    public string animationClipName;
    public float animationLength = 4;

    bool cutscenePlayed = false;

    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !cutscenePlayed)
        {
            PlayerController.instance.currentSize = Sizes.MEDIUM;
            PlayerController.instance.isBouncing = true;


            PlayerController.player.transform.position = cutscenePosition.position;
            PlayerController.player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            
            particles.SetActive(true);
            particlesAnimator.enabled = true;
            particlesAnimator.Play(animationClipName);
            
            cutscenePlayed = true;
            StartCoroutine(ReEnableMovement());
        }
    }

    IEnumerator ReEnableMovement()
    {
        yield return new WaitForSeconds(animationLength);
        
        PlayerController.instance.isBouncing = false;

        particles.SetActive(false);
        particlesAnimator.enabled = false;
    }
}
