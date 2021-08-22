using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PoolItem
{
    public GameObject prefab;
    public int amount;
    public bool expandable;
}

public class EnemyPool : MonoBehaviour 
{
    [SerializeField] private PersistentObject _persistentObject;

    public static EnemyPool Pool;
    public List<PoolItem> items;
    public List<GameObject> pooledItems;

    void Awake() 
    {

        Pool = this;

        pooledItems = new List<GameObject>();
        foreach(PoolItem item in items)
        {
            for (int i = 0; i < item.amount; i++)
            {
                GameObject obj = Instantiate(item.prefab);
                obj.transform.parent = transform;
                pooledItems.Add(obj);
//                obj.SetActive(false);
            }
        }
	}

    void OnEnable()
    {
        SceneLoader.SceneChanging += DisableAll;
    }

    public GameObject Get(string tag)
    {
        for (int i = 0; i < pooledItems.Count; i++)
        {
            if(!pooledItems[i].activeInHierarchy && pooledItems[i].tag == tag)
            {
                return pooledItems[i];
            }
        }

        foreach(PoolItem item in items)
        {
            if(item.prefab.tag == tag && item.expandable)
            {
                GameObject obj = Instantiate(item.prefab);
                obj.SetActive(false);
                pooledItems.Add(obj);
                return obj;
            }
        }

        return null;
    }


    public void DisableAll()
    {
        foreach(GameObject obj in pooledItems)
        {
            if(obj != null)
                obj.SetActive(false);
        }
    }


}
