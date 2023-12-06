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
}
