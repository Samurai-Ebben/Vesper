using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class DevButtons : MonoBehaviour
{
    GameObject player;
    Collider2D playerCollider2D;
    Rigidbody2D playerRB2D;
    float defaultGravity;

    SpawnAndCheckpoint spawnAndCheckpoint;
    SceneHandler sceneHandler;

    //Vector3 checkpoint;
    
    public bool amImmortal = false;
    public bool amGhost = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCollider2D = player.GetComponent<Collider2D>();
        playerRB2D = player.GetComponent<Rigidbody2D>();
        defaultGravity = playerRB2D.gravityScale;

        sceneHandler = GetComponent<SceneHandler>();
        
        spawnAndCheckpoint = GetComponent<SpawnAndCheckpoint>();
        Debug.Log(sceneHandler);
 
    }

    void Update()
    {
        
        // Restart Level
        if (Input.GetKeyUp(KeyCode.R))
        {
            DOTween.Clear();
            sceneHandler.ReloadScene();
            DOTween.Init();
        }

        // Next Level
        if (Input.GetKeyUp(KeyCode.N))
        {
            sceneHandler.NextScene();
        }

        // Previous Level
        if (Input.GetKeyUp(KeyCode.P))
        {
            sceneHandler.PreviousScene();
        }

        // Set Development Checkpoint
        if (Input.GetKeyUp(KeyCode.C))
        {
            //checkpoint = player.transform.position;
            spawnAndCheckpoint.SetCheckpoint();
        }

        // Teleport/respawn at Checkpoint
        if (Input.GetKeyUp(KeyCode.T))
        {
            //player.transform.position = checkpoint;
            spawnAndCheckpoint.RespawnPlayer();
        }

        //// Show/Hide UI (turn on/off renderer components)
        //if (Input.GetKeyUp(KeyCode.U))
        //{

        //}

        // Toggle Immortal Player
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            if(amImmortal != true)
            {
                amImmortal = true;
            }
            else
            {
                amImmortal = false;
            }
        }

        // NoClip (fly, go through walls)
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            if(amGhost != true)
            {
                playerCollider2D.enabled = false;
                playerRB2D.gravityScale = 0;
                amGhost = true;
            }
            else
            {
                playerCollider2D.enabled = true;
                playerRB2D.gravityScale = defaultGravity;
                amGhost = false;
            }
        }

        //// Kill all enemies
        //if (Input.GetKeyUp(KeyCode.Alpha3))
        //{

        //}

        // timeScale down
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            Time.timeScale -= 0.25f;        
        }

        // timeScale up
        if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            Time.timeScale += 0.25f;
        }
    }
}
