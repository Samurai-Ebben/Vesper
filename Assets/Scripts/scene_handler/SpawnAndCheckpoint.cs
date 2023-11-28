using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAndCheckpoint : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject playerPrefab;

    [SerializeField]
    Vector3 currentCheckpoint;

    GameObject player;

    void Awake()
    {
        currentCheckpoint = spawnPoint.position;
        SpawnPlayer();
    }

    void Update()
    {

    }

    public void SetCheckpoint(Vector3 coordinates)
    {
        currentCheckpoint = coordinates;
    }
    
    public void SpawnPlayer()
    {
        player = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
    }

    public void RespawnPlayer()
    {
        if (player == null)
        {
            SpawnPlayer();
        }
        else
        {
            player.transform.position = currentCheckpoint;
        }
    }
}
