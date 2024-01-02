using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RisingButton : MonoBehaviour, IReset
{
    // Takes in the moving platforms
    public List<RisingMovement> platforms;
   
    // Animator anim;
    public float pressedDistance;
    public float timer;
    private bool playerIsLarge;
    private bool onMe;
    private bool prevSize;
    public Transform box;
    private Vector3 stopPos;

    private void Start()
    {
        stopPos = Vector3.zero;
        RegisterSelfToResettableManager();
    }
    
    public void Update()
    {
        timer += Time.deltaTime;
        if(onMe)
        {
            if (playerIsLarge)
            {
                box.localPosition = Vector3.Lerp(Vector3.zero, Vector3.down * pressedDistance, timer);
            }
            else
            {
                box.localPosition = Vector3.Lerp(box.localPosition, Vector3.zero, Time.deltaTime * 4);
            }
        }
      
        else
        {
            box.localPosition = Vector3.Lerp(box.localPosition, Vector3.zero, Time.deltaTime * 4);
        }

        playerIsLarge = PlayerController.instance.currentSize == Sizes.LARGE;
        
        if(prevSize != playerIsLarge)
        {
            if(playerIsLarge == false)
            {
                stopPos = box.transform.localPosition;
            }
            timer = 0;
        }
        prevSize = playerIsLarge;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (onMe == false)
            {
                timer = 0;
                onMe = true;
            }

            foreach (var platform in platforms)
            {
                if (playerIsLarge)
                {
                    platform.Rise();
                }
                else
                {
                    platform.Descend();

                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var platform in platforms)
            {
                if (playerIsLarge)
                {
                    platform.Rise();
                }
                else
                {
                    platform.Descend();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (onMe == true)
            {
                onMe = false;
                timer = 0;
                stopPos = box.transform.localPosition;
            }
            foreach (var platform in platforms)
                platform.Descend();
        }
    }

    public void DescendPlatformsPromptly()
    {
        foreach (var platform in platforms)
        {
            platform.Descend();
            platform.ResetTimer();
        }
    }

    public void Reset()
    {
        stopPos = Vector3.zero;
        box.transform.localPosition = Vector3.zero;
    }

    public void RegisterSelfToResettableManager()
    {
        ResettableManager.Instance?.RegisterObject(this);
    }
}
