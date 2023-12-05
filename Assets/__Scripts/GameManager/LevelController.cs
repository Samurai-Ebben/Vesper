using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    //Singleton
    //public static LevelController instance;

    public GameObject spawnPoint;
    public GameObject playerPrefab;

    [SerializeField]
    Vector3 currentCheckpoint;

    GameObject playerHolder;
    [HideInInspector]public GameObject player;

    void Awake()
    {
        //if (instance != null) return;
        //instance = this;
        currentCheckpoint = spawnPoint.transform.position;
        SpawnPlayer();
    }

    public void SetCheckpoint()
    {
        currentCheckpoint = player.transform.position;
        //spawnPoint.transform.position = player.transform.position;
    }

    public void SpawnPlayer()
    {
        playerHolder = Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity);
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
            ResetAllPlatforms();
            //player.transform.position = spawnPoint.transform.position;
        }
    }
    public void ResetAllPlatforms()
    {
        IReset[] platforms = FindObjectsOfType<Component>().OfType<IReset>().ToArray();

        foreach (IReset platform in platforms)
        {
            platform.ResetPlatform();
        }
    }
}
