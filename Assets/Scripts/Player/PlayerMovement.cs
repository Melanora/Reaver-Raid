//using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{

	[Header("Speed")]
	[SerializeField] private float _speedFast = 0f;
	[SerializeField] private float _speedNormal = 0f;
	[SerializeField] private float _speedSlow = 0f;
	[SerializeField] private float _speedStrafe = 0f;
	[SerializeField] private float _acceleration = 0f;
    [SerializeField] private Vector2 _velocity;

	[Header("Positions")]
	[SerializeField] private float _levelSwitchThreshold;
	[SerializeField] private float _levelSwitchY;
	[SerializeField] private Vector2 _respawnPosition;

    public static float PositionY {get; private set;}
//    public static bool IsActive {get; private set;} = false;
    

	private Transform _transform;
    private Rigidbody2D _rigidBody2d;
    private float _velocityYLast;
    private FloatArgs _floatArgs;


//    public static event UnityAction<PlayerMovement> playerLoaded;
    public static event UnityAction PlayerLoaded;
    public static event UnityAction PlayerUnloading;
	public static event UnityAction LevelSwitch;
  	public static event UnityAction PlayerReset;
  	public static event UnityAction<float> SpeedChanged;


    void Awake()
    {


//        Debug.Log("Player awake!");

    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        _rigidBody2d.MovePosition(_rigidBody2d.position + _velocity*Time.fixedDeltaTime);
        GameManager.Instance.PlayerPositionY = _transform.position.y;

        if (_transform.position.y >= _levelSwitchThreshold)
        {
            Time.timeScale = 0f;
            Debug.Log("Player LevelSwitch.Invoke");
            LevelSwitch?.Invoke();
        }
    }

    public void ChangeVelocity(int vertical, int horizontal)
    {
        float v = (vertical != 0f)? vertical : Mathf.Clamp(_speedNormal - _velocity.y, -1f, 1f);

		_velocity.y = Mathf.Clamp(_velocity.y + _acceleration *v, _speedSlow, _speedFast) ;
        _velocity.x = _speedStrafe * horizontal;

        if(Mathf.Abs(_velocityYLast - _velocity.y) > 1f)
        {
//            _floatArgs.Value = _velocity.y / _speedFast;
            SpeedChanged?.Invoke(_velocity.y / _speedFast);
        }
        _velocityYLast = _velocity.y;
    }

	void OnLevelSwitch()
	{
		Vector3 pos = _transform.position;
		pos.y = _levelSwitchY;
		_transform.position = pos;
	}

    void OnCollisionEnter2D(Collision2D collision)
	{
        RRactor actor = collision.transform.GetComponent<RRactor>();
        if(actor != null)
        {
            GameManager.Instance.AddScore(actor.Points);
            actor.Despawn();
        }
		SpawnExplosion();
		PlayerReset?.Invoke();
	}

    void SpawnExplosion()
    {
        GameObject explosion = ExplosionPool.Pool.Get();
        if(explosion != null)
        {     
            explosion.transform.position = _transform.position;
            explosion.SetActive(true);              
        }        
    }

	void OnNoFuel()
	{
		PlayerReset?.Invoke();
	}

	void OnPlayerReset()
	{
		_transform.position = _respawnPosition;
        _velocity = Vector2.zero;
		ExplosionPool.Pool.DisableAll();
	}

    void OnEnable()
    {
        _rigidBody2d = GetComponent<Rigidbody2D>();
		_transform = transform;
        _floatArgs = new FloatArgs();

    	PlayerReset += OnPlayerReset;
		LevelSwitch += OnLevelSwitch;
        PlayerFuel.NoFuel += OnNoFuel;

        Debug.Log("Player enabled!");
        PlayerLoaded?.Invoke();        
    }

    void Start()
    {
    }

    void OnDisable()
    {
        Debug.Log("Player disabling! PlayerUnloading.Invoke");        
    	PlayerReset -= OnPlayerReset;
		LevelSwitch -= OnLevelSwitch;
        PlayerFuel.NoFuel -= OnNoFuel;

        PlayerUnloading?.Invoke();
    }


}
