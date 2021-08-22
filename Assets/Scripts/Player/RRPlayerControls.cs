using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class RRPlayerControls : MonoBehaviour 
{
	[SerializeField] private bool _inMenu = false;
	[Header("Speed")]
	[SerializeField] private float _speedFast = 0f;
	[SerializeField] private float _speedNormal = 0f;
	[SerializeField] private float _speedSlow = 0f;
	[SerializeField] private float _speedStrafe = 0f;
	[SerializeField] private float _acceleration = 0f;

	[Header("Positions")]
	[SerializeField] private float _levelSwitchThreshold;
	[SerializeField] private float _levelSwitchY;
	[SerializeField] private Vector2 _respawnPosition;

	[Header("Sprites")]
	[SerializeField] private Sprite _sprForward;
	[SerializeField] private Sprite _sprLeft;
	[SerializeField] private Sprite _sprRight;
	
	[Header("Clips")]
	[SerializeField] private AudioClip _clipEngine;

	
	[Header("References")]
//	[SerializeField] private RRPlayerMissile _missile;
//	[SerializeField] private GameObject _bridge;
//	private RRBridge _bridgeScript;

	private Transform _transform;
	private SpriteRenderer _renderer;
	private AudioSource _asEngine;


//	private Tilemap _tilemap;
	private Rigidbody2D _rigidBody;
	private Vector3 _offset;
	private Vector2 _velocity = Vector2.zero;
//	private	float _speed;

//	private int _levelReached = 0;
//	private bool _isRefueling;
//	private RenderTexture rt;
//	private	Texture2D tex;

//	public event EventHandler OnGamePause;
	public event EventHandler LevelSwitch;
	public event EventHandler PlayerReset;
	public event EventHandler<MissileArgs> FireMissile;


	void Awake () 
	{
		Application.targetFrameRate = 60;
 		Screen.SetResolution(960, 540, false); 

//		_bridgeScript = _bridge.GetComponent<RRBridge>();

		_renderer = GetComponent<SpriteRenderer>();
        _rigidBody = GetComponent<Rigidbody2D>();
		_transform = transform;

		_velocity.x = 0;
		_velocity.y = _speedSlow;
//		_speed = _speedSlow;
		_offset = Vector3.zero;

//		LevelSwitch += OnLevelSwitch;
//		GetComponent<PlayerFuel>().NoFuel += OnNoFuel;

		if(!_inMenu)
		{
//			PlayerReset += OnPlayerReset;
//			_asEngine = Helpers.CreateAudioSource(gameObject, _clipEngine, 0.6f, true);
		}

	}

/*
	void Update()
	{
		GetInput();
		UpdateSprite();
	}
*/
/*
	void FixedUpdate () 
	{
		Fly();
	}
*/
/*
    private void GetInput()
    {
		if(Input.GetAxisRaw("Fire1") != 0)
			FireMissile?.Invoke(this, new MissileArgs(_rigidBody.position, _rigidBody.velocity.y));

		float v = Input.GetAxisRaw("Vertical");
		if(v == 0)
			v = Mathf.Clamp(_speedNormal - _velocity.y, -1, 1);

		_velocity.y = Mathf.Clamp(_velocity.y +_acceleration*v, _speedSlow, _speedFast);
        _velocity.x = _speedStrafe * Input.GetAxisRaw("Horizontal");
    }
*/
/*
	void Fly()
    {
        if (_transform.position.y >= _levelSwitchThreshold)
        {
            Time.timeScale = 0;
            LevelSwitch?.Invoke(this, EventArgs.Empty);
        }

		if(_inMenu)
		{
			_velocity.y = _speedNormal;
			_velocity.x = 0;
		}
		else
        	_asEngine.pitch = (_velocity.y / _speedFast) / 2 + 0.5f;

        _rigidBody.velocity = _velocity;

    }
*/
/*
	void UpdateSprite()
	{
        if (_velocity.x < 0)
            _renderer.sprite = _sprLeft;
        else if (_velocity.x > 0)
            _renderer.sprite = _sprRight;
        else
            _renderer.sprite = _sprForward;
	}
*/	
/*
    void OnCollisionEnter2D(Collision2D collision)
	{
        RRactor actor = collision.transform.GetComponent<RRactor>();
        if(actor != null)
        {
            GameManager.AddScore(actor.GetPoints());
            actor.Despawn(this, EventArgs.Empty);
        }
		SpawnExplosion();
		PlayerReset?.Invoke(this, EventArgs.Empty);

	}
*/
/*
	void OnPlayerReset(object sender, EventArgs e)
	{
		_transform.position = _respawnPosition;
		_rigidBody.velocity = Vector2.zero;
		ExplosionPool.Pool.DisableAll();
	}
*/	
/*
	void OnLevelSwitch(object sender, EventArgs e)
	{
		Vector3 pos = _transform.position;
		pos.y = _levelSwitchY;
		_transform.position = pos;

	}
*/
/*
	void OnNoFuel(object sender, EventArgs e)
	{
		PlayerReset?.Invoke(this, EventArgs.Empty);
	}
*/
/*
    void SpawnExplosion()
    {
        GameObject explosion = ExplosionPool.Pool.Get();
        if(explosion != null)
        {     
            explosion.transform.position = _transform.position;
            explosion.SetActive(true);              
        }        
    }
*/


}
