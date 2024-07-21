using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class LvlSelectorManager : MonoBehaviour
{

    public static LvlSelectorManager Instance;
    public int levelIndex;
    // List to store the state of each level
    public List<LevelState> levels;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }

        LoadLevelStates();
    }
    private void Start() {
        Time.timeScale = 1.0f;

        LvlSelectorManager.Instance.levels[levelIndex].entered = true;
        LvlSelectorManager.Instance.SaveLevelStates();
    }
    public void SaveLevelStates() {
        for (int i = 0; i < levels.Count; i++) {
            PlayerPrefs.SetInt("Level" + i, levels[i].entered ? 1 : 0);
            PlayerPrefs.SetInt("Collectible" + i, levels[i].collectibleCollected ? 1 : 0);
        }
    }

    // Method to load the level states from player preferences
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
}
    [System.Serializable]
    public class LevelState {
        public bool entered;
        public bool collectibleCollected;
    }

