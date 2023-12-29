using DG.Tweening;
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

    public List<TextMeshProUGUI> menuTxts = new List<TextMeshProUGUI>();
    TextMeshProUGUI txt;
    Color buttonOrigiColor;
    Vector3 btnOrigSize;

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
        SceneManager.LoadScene(1);
    }
    public void NavigateBtns()
    {
        var selected = events.currentSelectedGameObject.transform;
        txt = (TextMeshProUGUI)selected.GetComponentInChildren(typeof(TextMeshProUGUI));

        for (int i = 0; i < menuTxts.Count; i++)
        {
            menuTxts[i].DOColor(buttonOrigiColor, 0.1f).SetEase(Ease.InSine);
            menuTxts[i].transform.DOScale(btnOrigSize, 0.1f).SetEase(Ease.InSine);
        }

        DOTween.defaultTimeScaleIndependent = true;
        txt.transform.DOScale(btnOrigSize * 1.3f, 0.1f).SetEase(Ease.InSine);
        txt.DOColor(Color.white, 0.1f).SetEase(Ease.InSine);
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
