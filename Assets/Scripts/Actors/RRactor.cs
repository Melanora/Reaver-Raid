using System;
using UnityEngine;

[RequireComponent(typeof(RRpatrol))]
public class RRactor : MonoBehaviour
{
//    [SerializeField] private Transform _level;
//    [SerializeField] private GameObject _explosion;
    [SerializeField] private int _points;
    public int Points { get => _points; }

//    private AudioSource _as;
    private RRpatrol _rrPatrol;

    void Awake()
    {
        _rrPatrol = GetComponent<RRpatrol>();
    }

    void Start()
    {
        gameObject.SetActive(false);
    }


    public void Init(Transform level, PatrolType patrolType, float activationDist, Vector3 startPos)
    {
//		_level = level;
        transform.parent = level;
        _rrPatrol.SetPatrol(patrolType, activationDist, startPos);
    }

    public void Despawn()
	{
        gameObject.SetActive(false);
	}

    void OnDisable()
    {
//        transform.parent = EnemyPool.Pool.transform;        
    }

}
