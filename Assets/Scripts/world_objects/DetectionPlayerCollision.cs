using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionPlayerCollision : MonoBehaviour
{
    public bool isColliding;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isColliding = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isColliding = false;
    }
}
