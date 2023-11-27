using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SpawnAndCheckpoint spawnAndCheckpoint = FindObjectOfType<SpawnAndCheckpoint>();
            spawnAndCheckpoint.RespawnPlayer();
        }
    }
}
