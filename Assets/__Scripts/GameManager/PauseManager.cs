using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    private InputActionAsset actions;
    EventSystem events;
    bool isPaused = false;

    GameObject player;
    public GameObject PauseMenuCanvas;

    private void Awake()
    {
        player = PlayerController.player;
    }

    private void Start()
    {
        actions = player.GetComponent<PlayerInput>().actions;
        actions["Pause"].performed += OnPause;
        //actions["PauseTrigger"].canceled += OnPause;
        actions.Enable();
    }

    private void OnDisable()
    {
        actions["Pause"].performed -= OnPause;

        actions.Disable();
    }

    public void OnPause(InputAction.CallbackContext ctx)
    {
        PauseTrigger();
    }

    public void PauseTrigger()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0: 1;
        PauseMenuCanvas.SetActive(isPaused);
    }

    public void Replay()
    {
        PauseTrigger();
        LevelController.instance.RespawnPlayer();
    }
}
