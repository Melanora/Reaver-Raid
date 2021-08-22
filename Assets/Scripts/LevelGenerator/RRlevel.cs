using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Tilemap))]
public class RRlevel : MonoBehaviour
{
    [SerializeField] private PersistentObject _persistentObject;

// 	[SerializeField] private bool _inMenu = false;
    [SerializeField] private float _topLevelY;
    [SerializeField] private float _bottomLevelY;
    [SerializeField] private bool _isTopLevel;
    private Transform _transform;
    private Tilemap _tilemap;
//    private LevelGenerator _levelGenerator;
    [SerializeField] private int _currentLevel;

    public event EventHandler NewTopLevel;

//    private PlayerMovement _playerMovement;

    
    void Awake()
    {
        _transform = transform;
        _tilemap = GetComponent<Tilemap>();
        PlayerMovement.PlayerLoaded += OnPlayerLoaded;
//        PlayerMovement.PlayerUnloading += OnPlayerUnloading;
        SceneLoader.SceneChanging += OnSceneChanging;

    }

    void Start()
    {
//        _currentLevel = (_isTopLevel)? GameManager.Instance.LevelReached: GameManager.Instance.LevelReached-1;
        _currentLevel = (_isTopLevel)? 1: 0;

//        Debug.Log($"Start. Generating level {_currentLevel}. Is top: {_isTopLevel}");
        GenerateStartingLevel();
    }

    void GenerateStartingLevel()
    {
        if(_isTopLevel)
        {
            _currentLevel = GameManager.Instance.LevelReached;
            LevelGeneratorS.Generator.GenerateLevel(_currentLevel, _tilemap, _transform);
        }
    }

    void OnPlayerLoaded()
    {
        Debug.Log("RRLevel: Subscribing to player events");
        PlayerMovement.LevelSwitch += OnLevelSwitch;
        PlayerMovement.PlayerReset += OnPlayerReset;
//        GenerateStartingLevel();
    }

    void OnSceneChanging()
    {
        Debug.Log("RRLevel: Unsubscribing from player events");
        PlayerMovement.LevelSwitch -= OnLevelSwitch;
        PlayerMovement.PlayerReset -= OnPlayerReset;
    }

    private void OnLevelSwitch()
    {
        Debug.Log("Switching level");
        _isTopLevel = !_isTopLevel;

        if(_isTopLevel)
        {
            _transform.position = new Vector3(0, _topLevelY, 0);
            _currentLevel += 2;
            _transform.name = "Level"+_currentLevel;
//            Debug.Log($"Level switch. Generating level {_currentLevel}. Is top: {_isTopLevel}");
            LevelGeneratorS.Generator.GenerateLevel(_currentLevel, _tilemap, _transform);
//            NewTopLevel?.Invoke(this, EventArgs.Empty);
        }
        else
            {
                _transform.position = new Vector3(0, _bottomLevelY, 0);                
            }

      	Time.timeScale = 1;
    }

    private void OnPlayerReset()
    {
        RegenerateLevel();
    }

    private void RegenerateLevel()
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);

//        _currentLevel = (_isTopLevel)? GameManager.Instance.LevelReached: GameManager.Instance.LevelReached-1;
//        LevelGeneratorS.Generator.GenerateLevel(level, _tilemap, _transform);

        if(_isTopLevel)
        {
//            Debug.Log($"Renerating level {_currentLevel}. Is top: {_isTopLevel}");
            _currentLevel = GameManager.Instance.LevelReached;
            LevelGeneratorS.Generator.GenerateLevel(_currentLevel, _tilemap, _transform);
        }
    }


}
