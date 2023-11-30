using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rising : MonoBehaviour
{
    //Takes in the moving platforms
    public List<RisingMovement> platforms;
    PlayerController player;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<PlayerController>();
            if (player.currentSize == Sizes.LARGE)
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

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        StartCoroutine(DelayDescend());
    //    }
    //}

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //var player = other.gameObject.GetComponent<PlayerController>();            

            if (player.currentSize == Sizes.LARGE)
            {
                foreach (var platform in platforms)
                {
                    if (!platform.isRising)
                    {
                        platform.Rise();
                    }
                }
                print("Large is on");
            }
            else
            {
                foreach (var platform in platforms)
                {
                    platform.Descend();
                }
                print("Large is off");

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
