using UnityEngine;

public class LevelController : MonoBehaviour
{
    //Singleton
    public static LevelController instance;

    public GameObject spawnPoint;
    public GameObject playerPrefab;

    [SerializeField]
    Vector3 currentCheckpoint;

    GameObject playerHolder;
    public GameObject Player { get; private set; }

    void Awake()
    {
        if (instance != null) return;
        instance = this;
        currentCheckpoint = spawnPoint.transform.position;
        SpawnPlayer();
    }

    public void SetCheckpoint()
    {
        currentCheckpoint = Player.transform.position;
        //spawnPoint.transform.position = player.transform.position;
    }

    public void SpawnPlayer()
    {
        playerHolder = Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity);
        Player = playerHolder.GetComponentInChildren<PlayerController>().gameObject;

    }

    public void RespawnPlayer()
    {
        if (Player == null)
        {
            SpawnPlayer();
        }
        else
        {
            Player.transform.position = currentCheckpoint;
            ResettableObjectManager.Instance.ResetAllObjects();

            //ResetAllPlatforms();
            //player.transform.position = spawnPoint.transform.position;
        }
    }
    //public void ResetAllPlatforms()
    //{
    //    IReset[] platforms = FindObjectsOfType<Component>().OfType<IReset>().ToArray();

    //    foreach (IReset platform in platforms)
    //    {
    //        platform.Reset();
    //    }
    //}
}
