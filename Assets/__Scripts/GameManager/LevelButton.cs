using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public int levelIndex;
    public Sprite collectibleSprite;
    private TextMeshProUGUI lvlTxt;
    private Button button;
    private Image buttonImage;


    void Start() {
        button = GetComponent<Button>();
        lvlTxt = GetComponentInChildren<TextMeshProUGUI>();
        buttonImage = GetComponent<Image>();
    }
    private void Update() {
        
        UpdateButton();
    }

    public void UpdateButton() {
        LevelState state = LvlSelectorManager.Instance.levels[levelIndex];
        if (!state.entered) {
            button.interactable = false;
        }
        else if (state.collectibleCollected) {
            button.interactable = true;
            if(!state.selected)
                lvlTxt.color = Color.black;
            buttonImage.sprite = collectibleSprite;
        }
        else {
            button.interactable = true;
        }
    }

}
