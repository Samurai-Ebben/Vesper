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

    //PlayerController player;
    public GameObject PauseMenuCanvas;

    private void Awake()
    {
    }

    private void Start()
    {
        var playerInput = PlayerController.player.GetComponent<PlayerInput>();
        actions = playerInput.actions;
        actions["Pause"].performed += OnPause;

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
        GameManager.Instance.RespawnPlayer();
    }
}
