using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappearing : MonoBehaviour
{
    public float sustainTime = 1f;
    public float cooldown = 0.5f;

    public Color32 onTriggerColor;
    private Color32 defaultColor;
    
    bool platformActive = true;
    bool ongoingCoroutine;

    public GameObject platform;
    Collider2D platformCollider;
    SpriteRenderer platformSpriteRenderer;

    GameObject player;
    Collider2D playerCollider;
    
    void Start()
    {
        platformSpriteRenderer = platform.GetComponent<SpriteRenderer>();
        platformCollider = platform.GetComponent<Collider2D>();

        player = GameManager.instance.player;    
        playerCollider = player.GetComponent<Collider2D>();

        defaultColor = platformSpriteRenderer.color;
    }

    private void Update()
    {
        if (!platformActive)
        {
            platform.SetActive(false);
        }
        else if (PlayerIsInside())
        {
            platform.SetActive(true);
        }
    }

    public void Disappear()
    {
        if (!ongoingCoroutine)
        {
            StartCoroutine(DisappearAndComeBack());
        }
    }

    IEnumerator DisappearAndComeBack()
    {
        ongoingCoroutine = true;
  
        platformSpriteRenderer.color = onTriggerColor;

        yield return new WaitForSeconds(sustainTime);
        platformActive = false;
        
        yield return new WaitForSeconds(cooldown);
        platformActive = true;

        platformSpriteRenderer.color = defaultColor;
        
        ongoingCoroutine = false;
    }

    bool PlayerIsInside()
    {
        if (playerCollider != null && platformCollider != null)
        {
            return playerCollider.IsTouching(platformCollider);
        }
        return false;
    }
}
