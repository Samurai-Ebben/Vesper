using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public TextMeshProUGUI collectibleDisplay;
    int collectibleAmount;
    int collectibleAmountTotal = 10;

    void Start()
    {
        collectibleAmount = PlayerPrefs.GetInt("Collectible");
        UpdateDisplay();
    }

    public void CollectibleCollected()
    {
        collectibleAmount++;
        PlayerPrefs.SetInt("collectibleAmount", collectibleAmount);
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        collectibleDisplay.text = collectibleAmount.ToString() + " / " + collectibleAmountTotal;
    }
}
