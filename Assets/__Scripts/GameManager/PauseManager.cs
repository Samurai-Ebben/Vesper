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

    public GameObject PauseMenuCanvas;
    public GameObject menu;
    public GameObject controls;
    public GameObject indicator;
    public GameObject indicator2;

    public float indicatorSpaceing = 20;

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


        for (int i = 0; i < menuTxts.Count; i++)
        {
            menuTxts[i].DOColor(buttonOrigiColor, fadeTime).SetEase(Ease.InSine);
            menuTxts[i].transform.DOScale(btnOrigSize, fadeTime).SetEase(Ease.InSine);
        }

        txt = (TextMeshProUGUI)selected.GetComponentInChildren(typeof(TextMeshProUGUI));
        DOTween.defaultTimeScaleIndependent = true;
        txt.transform.DOScale(btnOrigSize * 1.3f, fadeTime).SetEase(Ease.InSine);
        txt.DOColor(Color.white, fadeTime).SetEase(Ease.InSine);
        //Transform textsLen = events.currentSelectedGameObject.gameObject.GetComponentInChildren<TextMeshProUGUI>().transform;

        //UpdateIndicators(textsLen);
    }

    private void UpdateIndicators(Transform selected) {
        RectTransform selectedRectTransform = selected.GetComponent<RectTransform>();
        float width = selectedRectTransform.rect.width;

        Vector3 leftIndicatorPosition = selected.position - new Vector3(width /indicatorSpaceing, 0, 0);
        Vector3 rightIndicatorPosition = selected.position + new Vector3(width / indicatorSpaceing, 0, 0);

        indicator.transform.position = leftIndicatorPosition;
        indicator2.transform.position = rightIndicatorPosition;
    }

    public void PauseTrigger()
    {
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
        controls.SetActive(true);
        events.SetSelectedGameObject(controls.GetComponentInChildren<Button>().gameObject);

    }

    public void BackMenu()
    {
        menu.SetActive(true);
        events.SetSelectedGameObject(events.firstSelectedGameObject);

        controls.SetActive(false);
    }

    public void Quit()
    {
        GameManager.Instance.RespawnPlayer();

        SceneManager.LoadScene(0);
    }
}
