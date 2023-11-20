using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevButtons : MonoBehaviour
{
    void Update()
    {
        // Restart Level
        if (Input.GetKeyUp(KeyCode.R))
        {
            
        }

        // Next Level
        if (Input.GetKeyUp(KeyCode.N))
        {

        }

        // Previous Level
        if (Input.GetKeyUp(KeyCode.P))
        {

        }

        // Set Development Checkpoint
        if (Input.GetKeyUp(KeyCode.C))
        {

        }

        // Teleport to Checkpoint
        if (Input.GetKeyUp(KeyCode.T))
        {

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
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            Time.timeScale += 0.25f;
        }
    }
}
