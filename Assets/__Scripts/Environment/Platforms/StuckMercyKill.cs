using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuckMercyKill : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && other.gameObject.CompareTag("Stuck")){
            GameManager.Instance.Death();
            print("death");
        }
    }
}
