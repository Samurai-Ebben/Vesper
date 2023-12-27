using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    private EventSystem events;
    public bool isPaused = false;
    //PlayerController player;
    public GameObject PauseMenuCanvas;
    public GameObject menu;
    public GameObject controls;
    public GameObject indicator;
    public GameObject indicator2;
    public float indicateOffset = .2f;
    float txtWidth;

    public List<TextMeshProUGUI> menuTxts = new List<TextMeshProUGUI>();
    TextMeshProUGUI txt;
    Color buttonOrigiColor;
    private void Start()
    {   
        events = GameManager.Instance.GetComponentInChildren<EventSystem>();

        isPaused = false;
        controls.SetActive(false);
        PauseMenuCanvas.SetActive(false);

        for (int i = 0; i < menuTxts.Count; i++)
        {
            buttonOrigiColor = menuTxts[i].color;
        }
    }

    private void Update()
    {

        var selected = events.currentSelectedGameObject.transform;
        txt = (TextMeshProUGUI)selected.GetComponentInChildren(typeof(TextMeshProUGUI));
        txtWidth = txt.preferredWidth;
        for (int i = 0; i < menuTxts.Count; i++)
        {
            menuTxts[i].color = buttonOrigiColor;
        }
        txt.color = Color.white;


        float scaledTxtWidth = txtWidth;
        indicator.transform.position = selected.position + new Vector3(scaledTxtWidth / 75 + indicateOffset, 0);
        indicator2.transform.position = selected.position - new Vector3(scaledTxtWidth / 75 + indicateOffset, 0);

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
#if UNITY_EDITOR
        print("Back to main menu");
#endif
        Application.Quit();
    }
}
