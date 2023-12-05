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
        player = LevelController.instance.Player;
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
        actions["PauseTrigger"].performed -= OnPause;

        actions.Disable();
    }

    void OnPause(InputAction.CallbackContext ctx)
    {
        isPaused = !isPaused;
        PauseTrigger();
    }

    public void PauseTrigger()
    {
        Time.timeScale = isPaused ? 0: 1;
        PauseMenuCanvas.SetActive(isPaused);
    }
}
