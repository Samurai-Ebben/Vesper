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
        //actions["Pause"].canceled += OnPause;
        actions.Enable();
    }

    private void OnDisable()
    {
        actions["Pause"].performed -= OnPause;

        actions.Disable();
    }
    void OnPause(InputAction.CallbackContext ctx)
    {
        isPaused = !isPaused;
        Pause();
        print("pressed");
    }

    void Pause()
    {
        Time.timeScale = isPaused ? 0: 1;
        PauseMenuCanvas.SetActive(isPaused);
    }
}
