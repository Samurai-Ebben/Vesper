using UnityEngine;

public class LevelController : MonoBehaviour
{
    //Singleton
    public static LevelController instance;

    [SerializeField]
    Vector3 currentCheckpoint;

    public GameObject startPoint;
    public GameObject playerHolderPrefab;
    public GameObject player { get; private set; }

    private void Awake()
    {
        if (instance != null) return;
        instance = this;

        if (PlayerController.player != null) 
        {
            player = PlayerController.player;
        }
        else
        {
            SpawnPlayer();
        }
    }

    void Start()
    {
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
}
