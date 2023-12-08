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

    public float deathTime;
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

        PlayerController.instance.rb.velocity = Vector2.zero;
        //PlayerController.instance.rb.gravityScale = 0;

        yield return new WaitForSeconds(deathTime);
        RespawnPlayer();
    }
}
