using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private EventSystem events;

    public GameObject MenuCanvas;
    public GameObject menu;
    public GameObject controls;
    public TextMeshProUGUI lvlsTxt;
    public GameObject lvlBtn;
    public List<TextMeshProUGUI> menuTxts = new List<TextMeshProUGUI>();
    public UnityEvent ButtonAction;
    public float fadeTime = 0.1f;

    bool canNav = false;

    Transform prevBtn;
    TextMeshProUGUI txt;

    Vector3 btnOrigSize;
    Color buttonOrigiColor;

    private void Start()
    {
        events = Camera.main.GetComponentInChildren<EventSystem>();

        Time.timeScale = 1;
        controls.SetActive(false);
        MenuCanvas.SetActive(true);

        if(PlayerPrefs.GetFloat("HavePlayed") > 0) {
            lvlBtn.SetActive(true);
            menuTxts.Add(lvlsTxt);
        }

        for (int i = 0; i < menuTxts.Count; i++)
        {
            buttonOrigiColor = menuTxts[i].color;
            btnOrigSize = menuTxts[i].transform.localScale;
        }
    }

    void Update()
    {
        NavigateBtns();
    }

    public void StartGame()
    {
        AudioManager.Instance.MenuSFX(AudioManager.Instance.startGameSound, AudioManager.Instance.startGameVolume);
        AudioManager.Instance.fadeOutVolumeInMenu = true; 
        RemoveAll();
        ButtonAction.Invoke();
    }

    public void LoadLevels() {
        SceneManager.LoadScene(19);
    }
    public void LoadIntro()
    {
        //PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(1);
    }

    private void RemoveAll()
    {
        DOTween.Clear();
    }

    public void StartNavigating()
    {
        canNav = true;
    }

    public void NavigateBtns()
    {
        if (!canNav) return;
        Transform selected = events.currentSelectedGameObject.transform;

        if(selected != prevBtn)
        {
            for (int i = 0; i < menuTxts.Count; i++)
            {
                menuTxts[i].DOColor(buttonOrigiColor, fadeTime).SetEase(Ease.InSine);
                menuTxts[i].transform.DOScale(btnOrigSize, fadeTime).SetEase(Ease.InSine);
            }
            txt = (TextMeshProUGUI)selected.GetComponentInChildren(typeof(TextMeshProUGUI));
            txt.transform.DOScale(btnOrigSize * 1.3f, fadeTime).SetEase(Ease.InSine);
            txt.DOColor(Color.white, fadeTime).SetEase(Ease.InSine);
            AudioManager.Instance.MenuSFX(AudioManager.Instance.clickInMenu, AudioManager.Instance.clickInMenuVolume);
        }

        prevBtn = selected;
        return;
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
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    private void OnApplicationQuit()
    {
        //PlayerPrefs.DeleteAll();
    }
}
