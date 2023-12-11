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
            player.bigEnabled = false;
            player.isBouncing = true;
            player.GetComponent<ventMovement>().enabled = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.GetComponent<ventMovement>().enabled = false;
        player.isBouncing = false;

    }

}
