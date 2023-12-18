using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;

public class Disappearing : MonoBehaviour, IReset
{
    public float sustainTime = 1f;
    public float cooldown = 0.5f;

    public Color32 onTriggerColor;
    private Color32 defaultColor;
    
    bool platformActive = true;
    bool previousActive =  true;
    bool playerOverlapping;
    bool ongoingCoroutine;
    Coroutine myCoroutine;

    public GameObject platform;
    SpriteRenderer platformSpriteRenderer;

    public UnityEvent fadeIn;
    public UnityEvent fadeOut;

    private void Awake()
    {
        platformSpriteRenderer = platform.GetComponent<SpriteRenderer>();
        defaultColor = platformSpriteRenderer.color;   
    }

    private void Start()
    {
        RegisterSelfToResettableManager();
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
            if (previousActive == false)
            {
                fadeIn.Invoke();
            }
        }

        previousActive = platformActive;
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
        previousActive = true;
        platformSpriteRenderer.color = defaultColor;
    }

    public void RegisterSelfToResettableManager()
    {
        ResettableManager.Instance?.RegisterObject(this);
    }
}