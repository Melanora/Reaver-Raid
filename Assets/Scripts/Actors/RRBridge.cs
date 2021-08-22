using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RRBridge : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;
    private GameObject _go;
    private AudioSource _as;

    private PlayerMovement _playerMovement;
/*
    void Awake()
    {
        _go = gameObject;
        _as = GameObject.Find("GeneralSFX").GetComponent<AudioSource>();
//        GameObject player = GameObject.FindGameObjectWithTag("Player");
//        PlayerMovement playerControls = player.GetComponent<PlayerMovement>();
//        SceneManager.sceneLoaded += OnSceneLoaded;
//        SceneManager.sceneUnloaded += OnSceneUnloaded;
        GameManager.Instance.PlayerLoaded += OnPlayerLoaded;
        GameManager.Instance.PlayerUnloading += OnPlayerUnloading;
        Disable();

    }

    void Start()
    {

    }

    void OnPlayerLoaded(object s, PlayerMovementArgs e)
    {
        _playerMovement = e.Value;
        _playerMovement.LevelSwitch += OnLevelSwitch;
        _playerMovement.PlayerReset += OnPlayerReset;
    }

    void OnPlayerUnloading(object s, EventArgs e)
    {
        _playerMovement.LevelSwitch -= OnLevelSwitch;
        _playerMovement.PlayerReset -= OnPlayerReset;
        _playerMovement = null;
    }

    public void OnLevelSwitch(object sender, EventArgs e)
    {
        Enable();
    }

    void OnPlayerReset(object sender, EventArgs e)
    {
        Disable();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.Instance.AdvanceLevel();
        _as.PlayOneShot(_clip, 1.5f);
        Disable();
    }

    public void Enable()
    {
        _go.SetActive(true);
    }

    public void Disable()
    {
        _go.SetActive(false);
    }

*/

}
