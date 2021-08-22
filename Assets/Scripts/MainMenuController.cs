using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private PersistentObject _persistentObject;


    void Awake()
    {

    }

    public void ExitBtn()
    {
        Application.Quit();
    }

    public void NewGameYesBtn()
    {


//        GameManager.NewGame();
//        SceneManager.LoadScene(1);
    }

    public void ContinueYesBtn()
    {
        if(PlayerPrefs.HasKey("LevelReached"))
        {
            _persistentObject.Continue();
            SceneLoader.Unload(1);
        }
        else
            NewGameYesBtn();

    }





}
