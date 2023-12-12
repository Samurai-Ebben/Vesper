using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentManager : MonoBehaviour
{
    PlayerController player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = PlayerController.instance;
            player.isBouncing = true;
            player.GetComponent<VentMovement>().enabled = true;
            //player.enabled = false;
        }        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.GetComponent<VentMovement>().enabled = false;
        StartCoroutine(DelayMovementDisable());

    }

    IEnumerator DelayMovementDisable()
    {
        yield return new WaitForSeconds(0.2f);
        player.isBouncing = false;
    }

   
}
