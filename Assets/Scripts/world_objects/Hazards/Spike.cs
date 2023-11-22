using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DevButtons devButtons = FindAnyObjectByType<DevButtons>();
            other.transform.position = devButtons.checkpoint;
        }
    }
}
