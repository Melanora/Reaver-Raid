using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletPoolItem
{
    public GameObject prefab;
    public int amount;
    public bool expandable;
}

public class BulletPool : MonoBehaviour 
{
    public static BulletPool Pool;
    public BulletPoolItem item;
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



}
