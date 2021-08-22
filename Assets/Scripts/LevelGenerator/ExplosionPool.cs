using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ExplosionPoolItem
{
    public GameObject prefab;
    public int amount;
    public bool expandable;
}

public class ExplosionPool : MonoBehaviour 
{
    public static ExplosionPool Pool;
    public ExplosionPoolItem item;
    public List<GameObject> pooledItems;

    void Awake() 
    {
        Pool = this;

        pooledItems = new List<GameObject>();
        for (int i = 0; i < item.amount; i++)
        {
            GameObject obj = Instantiate(item.prefab);
            obj.transform.parent = transform;
            obj.SetActive(false);
            pooledItems.Add(obj);
        }
	}

    public GameObject Get()
    {
        for (int i = 0; i < pooledItems.Count; i++)
        {
            if(!pooledItems[i].activeInHierarchy)
            {
                return pooledItems[i];
            }
        }

        if(item.expandable)
        {
            GameObject obj = Instantiate(item.prefab);
            obj.SetActive(false);
            pooledItems.Add(obj);
            return obj;
        }

        return null;
    }

    public void DisableAll()
    {
        foreach(GameObject go in pooledItems)
        {
            if(go != null)
                go.SetActive(false);
        }
    }



}
