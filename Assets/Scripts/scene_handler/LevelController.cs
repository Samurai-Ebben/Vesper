using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    //Singleton
    //public static LevelController instance;

    public Transform spawnPoint;
    public GameObject playerPrefab;

    [SerializeField]
    Vector3 currentCheckpoint;

    GameObject playerHolder;
    [HideInInspector]public GameObject player;

    void Awake()
    {
        //if (instance != null) return;
        //instance = this;
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
