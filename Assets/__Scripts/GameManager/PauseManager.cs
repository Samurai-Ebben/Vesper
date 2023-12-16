using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    //EventSystem events;
    public bool isPaused = false;

    //PlayerController player;
    public GameObject PauseMenuCanvas;

    private void Start()
    {
        isPaused = false;
        PauseMenuCanvas.SetActive(false);
    }

    public void PauseTrigger()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        PauseMenuCanvas.SetActive(isPaused);

        GetComponent<HideMouseCursor>().toggleCursorVisibility();
        print("canPause"); 
    }

    public void Replay()
    {
        PauseTrigger();
        GameManager.Instance.RespawnPlayer();
    }
}
