using System;
using UnityEngine;

public class GameManager //: MonoBehaviour
{
    public enum Difficulty {Easy = 0, Normal, Hardcore};
    public enum GameMode {Normal = 0, Reaver, Bomber};
    
    public int LevelReached {get; private set;} = 1;
    public int Score {get; private set;} = 0;
    public int LivesCurrent {get; private set;} = 3;

    public float PlayerPositionY;

	public event EventHandler<IntArgs> ScoreChanged;
	public event EventHandler<IntArgs> NewLevelReached;
	public event EventHandler<PlayerMovementArgs> PlayerLoaded;
	public event EventHandler PlayerUnloading;

    private int _livesNewGame = 3;
    private int _livesMax = 10;
    private Difficulty _difficulty;
    private GameMode _gameMode;
    private PlayerMovement _playerMovement;


    public static readonly GameManager Instance = new GameManager();


    public void AddScore(int points)
    {
        Score += points;
        ScoreChanged?.Invoke(null, new IntArgs(Score));

//        Debug.Log("Score: " + Score);
    }

    public void NewGame(GameMode newGameMode, Difficulty newDifficulty)
    {
        LevelReached = 1;
        Score = 0;
        _difficulty = newDifficulty;
        _gameMode = newGameMode;

        PlayerPrefs.SetInt("GameMode", (int)newGameMode);    
        PlayerPrefs.SetInt("Difficulty", (int)newDifficulty);    
        PlayerPrefs.SetInt("LevelReached", 1);
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("Lives", 3);        
        PlayerPrefs.Save();
    }

    public void ContinueSaved()
    {
        _difficulty = (Difficulty)PlayerPrefs.GetInt("GameMode");    
        _gameMode = (GameMode)PlayerPrefs.GetInt("Difficulty");    
        LivesCurrent = PlayerPrefs.GetInt("Lives");  
        LevelReached = PlayerPrefs.GetInt("LevelReached");
        Score = PlayerPrefs.GetInt("Score");   

    }

    public void AdvanceLevel()
    {
        LevelReached++;
//        Debug.Log("New level reached: " + LevelReached);
        NewLevelReached?.Invoke(null, new IntArgs(LevelReached));
    }

}
