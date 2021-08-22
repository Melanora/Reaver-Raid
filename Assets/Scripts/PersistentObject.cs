using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class PersistentObject : ScriptableObject
{

    public enum Difficulty {Easy = 0, Normal, Hardcore};
    public enum GameMode {Normal = 0, Reaver, Bomber};
    
    public int LevelReached {get; private set;} = 1;
    public int Score {get; private set;} = 0;
    public int LivesCurrent {get; private set;} = 3;

	public event UnityAction<int> ScoreChanged;
	public event UnityAction<int> NewLevelReached;
	public event UnityAction<PlayerMovementArgs> PlayerLoaded;
    public event UnityAction SceneUnloading;

    private int _livesNewGame = 3;
    private int _livesMax = 10;
    private Difficulty _difficulty;
    private GameMode _gameMode;
    private PlayerMovement _playerMovement;


//    public event UnityAction SceneChanging;

    void OnEnable()
    {
        Debug.Log("PersistentData OnEnable");
    }

    void OnDisable()
    {
        Debug.Log("PersistentData OnDisable");
    }


    public void AdvanceLevel()
    {
        LevelReached++;
        NewLevelReached?.Invoke(LevelReached);
    }

    public void AddScore(int points)
    {
        Score += points;
        ScoreChanged?.Invoke(Score);
//        Debug.Log("Score: " + Score);
    }

    public void NewGame(GameMode newGameMode, Difficulty newDifficulty)
    {
        LevelReached = 1;
        Score = 0;
        _difficulty = newDifficulty;
        _gameMode = newGameMode;
        SaveGame();    
//        SceneLoader.Load(2);
    }

    public void Continue()
    {
        LoadGame();
//        SceneLoader.Load(2);
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("GameMode", (int)_gameMode);    
        PlayerPrefs.SetInt("Difficulty", (int)_difficulty);    
        PlayerPrefs.SetInt("LevelReached", 1);
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("Lives", 3);        
        PlayerPrefs.Save();
    }

    private void LoadGame()
    {
        _gameMode       = (GameMode)PlayerPrefs.GetInt("Difficulty");    
        _difficulty     = (Difficulty)PlayerPrefs.GetInt("GameMode");    
        LevelReached    = PlayerPrefs.GetInt("LevelReached");
        Score           = PlayerPrefs.GetInt("Score");  
        LivesCurrent    = PlayerPrefs.GetInt("Lives");  
    }


}


