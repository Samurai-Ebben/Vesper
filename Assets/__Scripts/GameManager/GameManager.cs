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
    public GameObject Fade;
    AudioManager audioManager;
    private Vector2 instantiateCoordinate = new Vector3 (-25, -25);

    public float deathTime;
    public bool IsDead {  get; private set; }

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
        Fade.SetActive(false);

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
        IsDead = false; 
        PlayerController.instance.canMove = true;
        PlayerController.instance.GetComponentInChildren<SpriteRenderer>().enabled = true;
        player.transform.position = currentCheckpoint;
        ResettableManager.Instance.ResetAllObjects();
        GetComponent<CollectibleManager>().TriggerOnDeath();
    }
    
    public void Death()
    {
        Fade.SetActive(true);
        PlayerController.instance.canMove = false;
        PlayerController.instance.GetComponentInChildren<SpriteRenderer>().enabled = false;
        IsDead = true;
        audioManager.PlayingAudio(audioManager.death, audioManager.deathVolume);
        PlayerController.instance.VibrateController(.4f, .75f, .1f);

        StartCoroutine(DieDelay());
    }

    public IEnumerator DieDelay()
    {
        PlayerController.player.GetComponent<PlayerParticleEffect>().DeathParticle();

        PlayerController.instance.rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(deathTime);
        RespawnPlayer();
    }
}
