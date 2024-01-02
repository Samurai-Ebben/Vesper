using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

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

    public List<TextMeshProUGUI> menuTxts = new List<TextMeshProUGUI>();
    TextMeshProUGUI txt;
    Color buttonOrigiColor;
    Vector3 btnOrigSize;

    public float fadeTime = 0.1f;
    private void Start()
    {   
        events = GameManager.Instance.GetComponentInChildren<EventSystem>();

        isPaused = false;
        controls.SetActive(false);
        PauseMenuCanvas.SetActive(false);

        for (int i = 0; i < menuTxts.Count; i++)
        {
            buttonOrigiColor = menuTxts[i].color;
            btnOrigSize = menuTxts[i].transform.localScale;
        }
    }

    private void Update()
    {
        NavigateBtns();
    }

    public void NavigateBtns()
    {
        var selected = events.currentSelectedGameObject.transform;
        txt = (TextMeshProUGUI)selected.GetComponentInChildren(typeof(TextMeshProUGUI));


        for (int i = 0; i < menuTxts.Count; i++)
        {
            menuTxts[i].DOColor(buttonOrigiColor, fadeTime).SetEase(Ease.InSine);
            menuTxts[i].transform.DOScale(btnOrigSize, fadeTime).SetEase(Ease.InSine);
        }

        DOTween.defaultTimeScaleIndependent = true;
        txt.transform.DOScale(btnOrigSize * 1.3f, fadeTime).SetEase(Ease.InSine);
        txt.DOColor(Color.white, fadeTime).SetEase(Ease.InSine);
    }

    public void PauseTrigger()
    {
        AudioManager.Instance.MenuSFX(AudioManager.Instance.pauseMenu, AudioManager.Instance.pauseMenuVolume);
        isPaused = !isPaused;
        float timeNeeded = 0.01f;
        Time.timeScale = isPaused ? timeNeeded : 1;
        PauseMenuCanvas.SetActive(isPaused);
        for (int i = 0; i < menuTxts.Count; i++)
        {
            menuTxts[i].transform.DOScale(menuTxts[i].transform.localScale, 0.2f).SetEase(Ease.InOutBounce);
        }
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
