using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public EventSystem events;
    public bool isPaused = false;
    //PlayerController player;
    public GameObject PauseMenuCanvas;
    public GameObject menu;
    public GameObject controls;
    public GameObject indicator;
    public GameObject indicator2;
    public float indicateOffset = 50;

    private void Start()
    {
        isPaused = false;
        controls.SetActive(false);
        PauseMenuCanvas.SetActive(false);
        //events = GameManager.Instance.GetComponentInChildren<EventSystem>();
    }

    private void Update()
    {
        var selected = events.currentSelectedGameObject.transform;
        indicator.transform.position = selected.position - new Vector3(selected.localScale.x + indicateOffset, 0);
        indicator2.transform.position = selected.position - new Vector3(selected.localScale.x - (indicateOffset + 4), 0);
    }

    public void PauseTrigger()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        PauseMenuCanvas.SetActive(isPaused);

        GetComponent<HideMouseCursor>().toggleCursorVisibility();
    }

    public void Replay()
    {
        PauseTrigger();
        GameManager.Instance.RespawnPlayer();
    }

    public void ControlsMenu()
    {
        menu.SetActive(false);
        events.SetSelectedGameObject(controls.GetComponentInChildren<Button>().gameObject);
        controls.SetActive(true);
    }

    public void BackMenu()
    {
        menu.SetActive(true);
        events.SetSelectedGameObject(events.firstSelectedGameObject);

        controls.SetActive(false);
    }

    public void Quit()
    {
        print("Back to main menu");
    }
}
