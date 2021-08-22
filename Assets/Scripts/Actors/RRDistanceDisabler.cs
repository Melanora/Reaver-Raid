using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RRDistanceDisabler : MonoBehaviour
{
    [SerializeField] private float _disableDictance;
    private Transform _tr;

    void Awake()
    {
        _tr = transform;    
    }

    void Update()
    {
        if( (PlayerMovement.PositionY - _tr.position.y) > _disableDictance)
            gameObject.SetActive(false);
    }
}
