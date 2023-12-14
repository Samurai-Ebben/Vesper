using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.AI.Navigation;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public TextMeshProUGUI collectibleDisplay;

    List<GameObject> collectedObjects;
    
    int collectedAmount;
    int totalCollectibleAmount = 12;

    void Start()
    {
        PlayerPrefs.DeleteAll();
        collectedObjects = new List<GameObject>();
        collectedAmount = PlayerPrefs.GetInt("collectedAmount");
        UpdateDisplay();
    }

    // Save Collection Amount
    public void CollectibleCollected()
    {
        collectedAmount++;
        SaveNewCollectedAmount();
        UpdateDisplay();
    }
    private void SaveNewCollectedAmount()
    {
        PlayerPrefs.SetInt("collectedAmount", collectedAmount);
    }

    // Player Death Event
    public void TriggerOnDeath()
    {
        collectedAmount -= collectedObjects.Count;

        SaveNewCollectedAmount();
        RespawnCollectibles();
        UpdateDisplay();

        collectedObjects.Clear();
    }
    public void RespawnCollectibles()
    {
        foreach (GameObject obj in collectedObjects)
        {
            obj.SetActive(true);
        }
    }

    // Utility Functions
    public void UpdateDisplay()
    {
        collectibleDisplay.text = collectedAmount.ToString() + " / " + totalCollectibleAmount;
    }
    public void RegisterSelfAsCollected(GameObject collectible)
    {
        collectedObjects.Add(collectible);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }

//    private void OnDisable()
//    {
//#if UNITY_EDITOR
//        if (!UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
//        {
//            Debug.Log("Exiting Play Mode in Editor");
//            PlayerPrefs.DeleteAll();
//        }
//#endif
//    }
}
