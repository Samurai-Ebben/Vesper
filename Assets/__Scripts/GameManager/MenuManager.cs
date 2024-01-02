using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private EventSystem events;

    public GameObject MenuCanvas;
    public GameObject menu;
    public GameObject controls;

    Transform prevBtn;

    public List<TextMeshProUGUI> menuTxts = new List<TextMeshProUGUI>();
    TextMeshProUGUI txt;
    Color buttonOrigiColor;
    Vector3 btnOrigSize;

    public float fadeTime = 0.1f;
    private void Start()
    {
        events = Camera.main.GetComponentInChildren<EventSystem>();

        controls.SetActive(false);
        MenuCanvas.SetActive(true);

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
        RemoveAll();
        SceneManager.LoadScene(1);
    }

    private void RemoveAll()
    {
        DOTween.Clear();
    }

    public void NavigateBtns()
    {
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

        }

        prevBtn = selected;
        return;

        for (int i = 0; i < menuTxts.Count; i++)
        {
            menuTxts[i].DOColor(buttonOrigiColor, fadeTime).SetEase(Ease.InSine);
            menuTxts[i].transform.DOScale(btnOrigSize, fadeTime).SetEase(Ease.InSine);
        }

        DOTween.defaultTimeScaleIndependent = true;
        txt.transform.DOScale(btnOrigSize * 1.3f, fadeTime).SetEase(Ease.InSine);
        txt.DOColor(Color.white, fadeTime).SetEase(Ease.InSine);
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
