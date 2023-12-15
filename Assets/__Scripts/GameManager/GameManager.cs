using System.Collections;
using Unity.VisualScripting;
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
    AudioManager audioManager;
    private Vector2 instantiateCoordinate = new Vector3 (-25, -25);

    public float deathTime;
    public bool Dead {  get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
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
        audioManager = GetComponent<AudioManager>();
    }

    public void SetCheckpoint()
    {
        currentCheckpoint = player.transform.position;
    }

    public void SpawnPlayer()
    {
        Instantiate(playerHolderPrefab, instantiateCoordinate, Quaternion.identity);
        player = PlayerController.player;
    }

    public void RespawnPlayer()
    {
        player.transform.position = currentCheckpoint;
        ResettableManager.Instance.ResetAllObjects();
        GetComponent<CollectibleManager>().TriggerOnDeath();
        Dead = false;
    }
     
    public void Death()
    {
        Dead = true;
        StartCoroutine(DieDelay());
        audioManager.PlayingAudio(audioManager.death, audioManager.deathVolume);
    }

    public IEnumerator DieDelay()
    {
        PlayerController.player.GetComponent<PlayerParticleEffect>().DeathParticle();

        PlayerController.instance.rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(deathTime);
        RespawnPlayer();
    }
}
