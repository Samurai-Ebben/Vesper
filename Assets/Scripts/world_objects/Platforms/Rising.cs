using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rising : MonoBehaviour
{
    //Takes in the moving platforms
    public List<RisingMovement> platforms;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponent<PlayerController>();
            print(player.currentSize);
            if(player.currentSize == PlayerController.Sizes.LARGE)
            {
                foreach (var platform in platforms)
                {
                    platform.Rise();
                }
            }
            else
            {
                foreach (var platform in platforms)
                {
                    platform.Descend();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DelayDescend());
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponent<PlayerController>();
            if (player.currentSize == PlayerController.Sizes.LARGE)
            {
                foreach (var platform in platforms)
                {
                    if (!platform.isRising)
                    {
                        platform.Rise();
                    }
                }
            }
        }
    }

    IEnumerator DelayDescend()
    {
        yield return new WaitForSeconds(2);
        foreach (var platform in platforms)
        {
            platform.Descend();
        }
    }
}
