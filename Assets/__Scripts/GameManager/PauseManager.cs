using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    //EventSystem events;
    bool isPaused = false;

    //PlayerController player;
    public GameObject PauseMenuCanvas;

    //private void Update()
    //{
    //    OnPause();
    //}
    //public void OnPause()
    //{
    //    if(PlayerController.instance.pausedPressed)
    //        PauseTrigger();
    //}

    public void PauseTrigger()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        PauseMenuCanvas.SetActive(isPaused);
    }

    public void Replay()
    {
        PauseTrigger();
        GameManager.Instance.RespawnPlayer();
    }
}
