using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Disappearing : MonoBehaviour, IReset
{
    public float sustainTime = 1f;
    public float cooldown = 0.5f;

    public Color32 onTriggerColor;
    private Color32 defaultColor;
    
    bool platformActive = true;
    bool playerOverlapping;
    bool ongoingCoroutine;
    Coroutine myCoroutine;

    public GameObject platform;
    SpriteRenderer platformSpriteRenderer;

    private void Awake()
    {
        platformSpriteRenderer = platform.GetComponent<SpriteRenderer>();
        defaultColor = platformSpriteRenderer.color;   
    }
    void Start()
    {
    }

    private void Update()
    {
        if (!platformActive)
        {
            platform.SetActive(false);
        }
        else if (!playerOverlapping)
        {
            platform.SetActive(true);
        }
    }

    public void Disappear()
    {
        if (!ongoingCoroutine)
        {
            myCoroutine = StartCoroutine(DisappearAndComeBack());
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

    public void SetPlayerOverlapping(bool boolean)
    {
        playerOverlapping = boolean;
    }

    public void Reset()
    {
        if (ongoingCoroutine)
        {
            StopCoroutine(myCoroutine);
            ongoingCoroutine = false;
        }
        platformActive = true;
        platformSpriteRenderer.color = defaultColor;
    }
    private void OnEnable()
    {
        ResettableObjectManager.Instance?.RegisterObject(this);
    }

    private void OnDisable()
    {
        ResettableObjectManager.Instance?.UnregisterObject(this);
    }
}