using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LvlSelectorManager : MonoBehaviour
{

    public static LvlSelectorManager Instance;
    public int levelIndex;
    // List to store the state of each level
    public List<LevelState> levels;

    private EventSystem events;

    Transform prevBtn;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
        //PlayerPrefs.DeleteAll();
        LoadLevelStates();
    }

    private void Start() {
        events = EventSystem.current;
        Time.timeScale = 1.0f;
        LvlSelectorManager.Instance.levels[levelIndex].entered = true;
        LvlSelectorManager.Instance.SaveLevelStates();
    }

    private void Update() {
        NavigateBtns();
    }

    public void SaveLevelStates() {
        for (int i = 0; i < levels.Count; i++) {
            PlayerPrefs.SetInt("Level" + i, levels[i].entered ? 1 : 0);
            PlayerPrefs.SetInt("Collectible" + i, levels[i].collectibleCollected ? 1 : 0);
        }
    }

    public void LoadLevelStates() {
        for (int i = 0; i < levels.Count; i++) {
            levels[i].entered = PlayerPrefs.GetInt("Level" + i, 0) == 1;
            levels[i].collectibleCollected = PlayerPrefs.GetInt("Collectible" + i, 0) == 1;
        }
    }

    public void GoToLevel(int levelNum) {
        SceneManager.LoadScene(levelNum);
    }

    public void BackToMenu() {
        SceneManager.LoadScene(0);
    }

    public void NavigateBtns() {
        Transform selected = events.currentSelectedGameObject.transform;
        var selectedBtn = events.currentSelectedGameObject;
        LvlSelectorManager.Instance.levels[levelIndex].selected = true;

        selectedBtn.gameObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;

        // Scale up the newly selected button
        selectedBtn.gameObject.transform.localScale = Vector3.one * 1.2f;

        // Revert the previous button to its original size
        if (selected != prevBtn) {
            if (prevBtn != null) {
                LvlSelectorManager.Instance.levels[levelIndex].selected = false;
                prevBtn.gameObject.transform.localScale = Vector3.one; // Reset the scale
            }

            prevBtn = selected;
        }

        AudioManager.Instance.MenuSFX(AudioManager.Instance.clickInMenu, AudioManager.Instance.clickInMenuVolume);
        return;
    }

    public void SetCollectedGem(bool  collectedGem) {
        levels[levelIndex].collectibleCollected = collectedGem;
    }
}
    [System.Serializable]
    public class LevelState {
        public bool entered;
        public bool selected;
        public bool collectibleCollected;
    }

