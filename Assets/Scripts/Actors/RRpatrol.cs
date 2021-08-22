using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class RRpatrol : MonoBehaviour 
{

	[SerializeField] private PatrolType _patrolType;
	[SerializeField] private float _patrolActivationDist;
	[SerializeField] private float _speed = 0f;

	private Vector3 _startPosition;
	private float _startSpeed;
	public float Direction {get; private set;}
    
	private SpriteRenderer _sprend;
	private Rigidbody2D _rb;
	private Transform _tr;

	private Transform _playerTransform;
	private Vector2 _delta = Vector2.zero;

//	private GameObject _level;

	private RRlevel _level;


    void FixedUpdate () 
	{
//        if(!_rend.isVisible) return;

		float d;
		switch(_patrolType)
		{
			case PatrolType.No: 
				_delta.x = 0; 
				break;

			case PatrolType.Always: 
				if(_sprend.isVisible) 
					Move(); 
				else 
					Stop();
				break;

			case PatrolType.Proximity:
				d = _rb.position.y - GameManager.Instance.PlayerPositionY;
				if(d < _patrolActivationDist) 
					Move();
				else
					Stop();
				break;
				
			default: Stop(); break;
		}


	}

	public void SetPatrol(PatrolType patrolType, float activationDist, Vector3 startPos)
	{
		_patrolType = patrolType;
		_patrolActivationDist = activationDist;
		_startPosition = startPos;
		_tr.position = startPos;
	}

	void Move()
	{
		_delta.x = _speed * Direction;
		_rb.velocity = _delta;

		if(transform.CompareTag("Jet"))
		{
			if(Mathf.Abs(_startPosition.x - _tr.position.x) > Mathf.Abs(_startPosition.x*2))
				_tr.localPosition = _startPosition;
		}

	}

	void Stop()
	{
		_rb.velocity = Vector2.zero;		
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.CompareTag("Level")) // layer == LayerMask.NameToLayer ("Level"))
		{
			Direction = -Direction;
			_sprend.flipX = !_sprend.flipX;
		}
	}

	void OnEnable()
	{
//        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _sprend = GetComponent<SpriteRenderer>();
		_tr = transform;
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = Vector2.zero;
		_rb.position = _startPosition;

		if(_rb.tag != "Fuel")
		{
			_sprend.flipX = _rb.position.x > 0;
			Direction = (_rb.position.x >= 0)? -1f : 1f;
		}
		else
			_sprend.flipX = false;

	}


}
