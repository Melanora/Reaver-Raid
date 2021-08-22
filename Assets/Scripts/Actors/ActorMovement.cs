using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActorMovement : MonoBehaviour
{
	[SerializeField] private PatrolType _patrolType;
	[SerializeField] private float _patrolActivationDist;
	[SerializeField] private float _speed = 0f;

	public float Direction {get; private set;}

	private Vector3 _startPosition;
	private float _startSpeed;
	private SpriteRenderer _sprend;
	private Rigidbody2D _rb;
	private Transform _tr;
	private Transform _playerTransform;
	private Vector2 _delta = Vector2.zero;


    void OnEnable()
    {
        
    }

    void Update()
    {
        
    }
}
