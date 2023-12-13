using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RisingButton : MonoBehaviour, IReset
{
    //Takes in the moving platforms
    public List<RisingMovement> platforms;
   
    //  Animator anim;
    public float pressedDistance;
    private float timer;
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(onMe == false)
            {
                timer = 0;
                onMe = true;
            }
        
            foreach (var platform in platforms)
            {
                if(playerIsLarge)
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
            if(onMe == true)
            {
                onMe = false;
                timer = 0;
                stopPos = box.transform.localPosition;
            }
            foreach (var platform in platforms)
                platform.Descend();
        }
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
                box.localPosition = Vector3.Lerp(stopPos, Vector3.zero, timer);
            }
        }
      
        else
        {
            box.localPosition = Vector3.Lerp(stopPos, Vector3.zero, timer);
        }

        prevSize = playerIsLarge;
        playerIsLarge = PlayerController.instance.currentSize == Sizes.LARGE;
        if(prevSize != playerIsLarge)
        {
            if(playerIsLarge == false)
            {
                stopPos = box.transform.localPosition;
            }
            timer = 0;
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
