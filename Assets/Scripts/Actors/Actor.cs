using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PatrolType {No = 0, Always, Proximity};

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Actor : MonoBehaviour
{
    [SerializeField] private int _points;

    public int Points { get => _points; }


    
    protected abstract void Move();
    protected abstract void Attack();
    

}
