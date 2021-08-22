using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class TileVariants
{
    public TileBase tile;
    public TileBase[] variants;
}

public class TileSelector : MonoBehaviour
{
    public static TileSelector Selector;
    [SerializeField] private TileBase _defaultTile;
    [SerializeField] private TileBase _currentTile;
    [SerializeField] private List<TileVariants> _tileVariants;

    void Awake()
    {
        Selector = this;
    }

    public TileBase GetNewTile()
    {
        foreach(TileVariants item in _tileVariants)
        {
            if(item.tile.name == _currentTile.name)
            {
                TileBase newTile = item.variants[Random.Range(0, (item.variants.Length-1)*10)/10];
                _currentTile = newTile;
                return newTile;
            }
        }
        return _defaultTile;
    }
}

