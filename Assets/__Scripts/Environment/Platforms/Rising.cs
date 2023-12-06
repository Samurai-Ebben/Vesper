using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rising : MonoBehaviour
{
    //Takes in the moving platforms
    public List<RisingMovement> platforms;
    PlayerController player;

    public float dureationOnTarget = 3;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject.GetComponentInParent<PlayerController>();

            bool large = player.currentSize == Sizes.LARGE;

            foreach (var platform in platforms)
            {
                if(large)
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

            bool large = player.currentSize == Sizes.LARGE;

            foreach (var platform in platforms)
            {
                if (large)
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

    IEnumerator DelayDescend()
    {
        yield return new WaitForSeconds(dureationOnTarget);
        foreach (var platform in platforms)
        {
            platform.Descend();
        }
    }
}
