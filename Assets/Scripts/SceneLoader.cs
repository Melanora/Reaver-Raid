using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public static event UnityAction SceneChanging;

    void Awake()
    {
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        Load(1);
    }

    private void OnSceneUnloaded(Scene s)
    {        
        Debug.Log("SceneLoader: OnSceneUnloaded " + s.buildIndex);

        switch(s.buildIndex)
        {
            case 1: 
                Load(2);
                break;
            case 2: 
                Load(1);
                break;
            default: 
                Debug.Log($"Unexpected scene index ({s.buildIndex})"); 
                break;
        }
    }

    public static void Load(int index)
    {
        Debug.Log("SceneLoader: LoadScene " + index);
        SceneManager.LoadScene(index, LoadSceneMode.Additive);
    }

    public static void Unload(int index)
    {
        Debug.Log("SceneLoader: SceneChanging.Invoke | UnloadScene " + index);

        SceneChanging?.Invoke();
        SceneManager.UnloadSceneAsync(index);        
    }

}
