using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAndCheckpoint : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject playerPrefab;

    [SerializeField]
    Vector3 currentCheckpoint;

    GameObject playerHolder;
    GameObject player;

    void Awake()
    {
        currentCheckpoint = spawnPoint.position;
        SpawnPlayer();
    }

    public void SetCheckpoint()
    {
        currentCheckpoint = player.transform.position;
    }

    public void SpawnPlayer()
    {
        playerHolder = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
        player = playerHolder.GetComponentInChildren<Rigidbody2D>().gameObject;
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
