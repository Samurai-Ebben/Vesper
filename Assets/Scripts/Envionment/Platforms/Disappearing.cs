using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappearing : MonoBehaviour
{
    public float sustainTime = 1f;
    public float cooldown = 0.5f;

    public GameObject platform;
    SpriteRenderer platformSpriteRenderer;
    
    bool ongoingCoroutine;

    public Color32 onTriggerColor;
    private Color32 defaultColor;

    void Start()
    {
        platformSpriteRenderer = platform.GetComponent<SpriteRenderer>();
        defaultColor = platformSpriteRenderer.color;
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
        platform.SetActive(false);
        
        yield return new WaitForSeconds(cooldown);
        platform.SetActive(true);

        platformSpriteRenderer.color = defaultColor;
        
        ongoingCoroutine = false;
    }
}
