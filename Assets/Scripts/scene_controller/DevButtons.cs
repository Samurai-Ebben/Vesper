using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DevButtons : MonoBehaviour
{
    Vector3 checkpoint;
    GameObject player;
    Collider2D playerCollider2D;
    Rigidbody2D playerRB2D;
    float defaultGravity;

    public bool amGhost = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCollider2D = player.GetComponent<Collider2D>();
        playerRB2D = player.GetComponent<Rigidbody2D>();
        defaultGravity = playerRB2D.gravityScale;
    }

    void Update()
    {
        // Restart Level
        if (Input.GetKeyUp(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // Next Level
        if (Input.GetKeyUp(KeyCode.N))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        // Previous Level
        if (Input.GetKeyUp(KeyCode.P))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

        // Set Development Checkpoint
        if (Input.GetKeyUp(KeyCode.C))
        {
            checkpoint = player.transform.position;
            print("Checkpoint set to: " + checkpoint);
        }

        // Teleport to Checkpoint
        if (Input.GetKeyUp(KeyCode.T))
        {
            player.transform.position = checkpoint;
        }

        // Show/Hide UI (turn on/off renderer components)
        if (Input.GetKeyUp(KeyCode.U))
        {

        }

        // Toggle Immortal Player
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {

        }

        // NoClip (fly, go through walls)
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            if(amGhost != false)
            {
                playerCollider2D.enabled = true;
                playerRB2D.gravityScale = defaultGravity;
                amGhost = true;
            }
            else
            {
                playerCollider2D.enabled = false;
                playerRB2D.gravityScale = 0;
                amGhost = false;
            }
        }

        // Kill all enemies
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {

        }

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
