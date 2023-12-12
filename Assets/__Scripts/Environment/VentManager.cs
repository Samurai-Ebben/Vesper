using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentManager : MonoBehaviour
{
    PlayerController player;
    private bool isExiting = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = PlayerController.instance;
            player.isBouncing = true;
            player.GetComponent<ventMovement>().enabled = true;
            //player.enabled = false;
        }        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isExiting)
        {
            isExiting = true;
            StartCoroutine(DelayMovementDisable());
        }

    }

    IEnumerator DelayMovementDisable()
    {
        yield return new WaitForSeconds(0.2f);
        player.isBouncing = false;
        player.GetComponent<ventMovement>().enabled = false;
        isExiting = false;
    }

   
}
