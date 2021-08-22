using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] GameObject _PauseMenu;
    [SerializeField] private PersistentObject _persistentObject;
    private bool _isPaused;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !_isPaused)
        {
            Pause();
            _PauseMenu.SetActive(true);
        }
    }

    public void ExitYesBtn()
    {
        _persistentObject.SaveGame();
        Unpause();
        SceneLoader.Unload(2);        
    }

    public void ResumeBtn()
    {
        Unpause();
    }

    void Pause()
    {
        _isPaused = true;
        Time.timeScale = 0;
        AudioListener.pause = true;
    }

    void Unpause()
    {
        _isPaused = false;
        Time.timeScale = 1;
        AudioListener.pause = false;
    }



}
