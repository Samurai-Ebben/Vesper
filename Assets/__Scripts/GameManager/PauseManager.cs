using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    private InputActionAsset actions;
    EventSystem events;
    bool isPaused = false;

    public GameObject PauseMenuCanvas;
    private void Awake()
    {
        actions = GameManager.instance.player.GetComponent<InputActionAsset>();
        actions["Pause"].performed += OnPause;
    }

    void OnPause(InputAction.CallbackContext ctx)
    {
        isPaused = !isPaused;
    }

    void Pause()
    {
        PauseMenuCanvas.SetActive(isPaused);
    }
}
