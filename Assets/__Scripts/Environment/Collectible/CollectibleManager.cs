using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectibleManager : MonoBehaviour
{
    public TextMeshProUGUI collectibleDisplay;
    public ParticleSystem alertFx;
    public GameObject imgUI;
    List<GameObject> collectedObjects;

    int collectedAmount;
    //int totalCollectibleAmount = 12;

    void Start()
    {
        alertFx = GetComponentInChildren<ParticleSystem>();
        //alertFx.transform.position = imgUI.transform.position;
        ShowParticleEffectAtCanvasObject();
        collectedObjects = new List<GameObject>();
        collectedAmount = PlayerPrefs.GetInt("collectedAmount");
        UpdateDisplay();
    }

    // Save Collection Amount
    public void CollectibleCollected()
    {
        collectedAmount++;
        alertFx.Play();
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
    void ShowParticleEffectAtCanvasObject()
    {
        // Get the screen position of the canvas object
        RectTransform canvasRectTransform = imgUI.GetComponent<RectTransform>();
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, canvasRectTransform.position);

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 10f));
        alertFx.transform.position = new Vector3(worldPos.x, worldPos.y/1.05f);
    }

    // Utility Functions
    public void UpdateDisplay()
    {
        collectibleDisplay.text = " X " + collectedAmount.ToString();
    }
    public void RegisterSelfAsCollected(GameObject collectible)
    {
        collectedObjects.Add(collectible);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }

    //private void OnDestroy()
    //{
    //    DOTween.Clear(transform);
    //}

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
