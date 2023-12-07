using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Singleton
    public static GameManager Instance;

    [SerializeField]
    Vector3 currentCheckpoint;

    public GameObject startPoint;
    public GameObject playerHolderPrefab;
    private GameObject player;

    private void Awake()
    {
        if (Instance != null) return;
        Instance = this;
    }

    void Start()
    {
        if (PlayerController.player != null) 
        {
            player = PlayerController.player;
        }
        else
        {
            SpawnPlayer();
        }

        currentCheckpoint = startPoint.transform.position;
        RespawnPlayer();
    }

    public void SetCheckpoint()
    {
        currentCheckpoint = player.transform.position;
    }

    public void SpawnPlayer()
    {
        Instantiate(playerHolderPrefab);
        player = PlayerController.player;
    }

    public void RespawnPlayer()
    {
        player.transform.position = currentCheckpoint;
        ResettableObjectManager.Instance.ResetAllObjects();
    }
     
    public void Death()
    {
        StartCoroutine(DieDelay());
    }

    public IEnumerator DieDelay()
    {
        PlayerController.player.GetComponent<PlayerParticleEffect>().DeathParticle();

        yield return new WaitForSeconds(.3f);
        RespawnPlayer();
    }
}
