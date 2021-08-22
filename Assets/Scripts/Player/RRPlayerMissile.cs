using System;
using UnityEngine;

public class RRPlayerMissile : MonoBehaviour
{
    [SerializeField] private float _speed;
//    [SerializeField] private float _timeout;
//    [SerializeField] private float _maxDist;
//    [SerializeField] private float _levelSwitchOffset;
//    [SerializeField] private AudioClip _clipExplosion;
    [SerializeField] private float _offsetLimit;
    [SerializeField] private AudioClip _clipFire;

    public bool isAvailable {get; private set;}

    private Rigidbody2D _playerRb;
    private Rigidbody2D _rb;
    private Transform _tr;
    private SpriteRenderer _rend;
    private AudioSource _audioSource;
    private Vector2 _pos;
    private float _offsetFromPlayer;

//    private int _frame = 0;

    void Awake()
    {
    }

    void Start()
    {
        isAvailable = true;

        _rb = GetComponent<Rigidbody2D>();
        _tr = GetComponent<Transform>();
        _rend = GetComponent<SpriteRenderer>();

//        _playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
//        _playerRb.GetComponent<RRPlayerControls>().OnLevelSwitch += LevelSwitch;
//        _playerRb.GetComponent<RRPlayerControls>().FireMissile += OnFireMissile;

        _audioSource = GameObject.Find("GeneralSFX").GetComponent<AudioSource>();
        _playerRb = transform.parent.GetComponent<Rigidbody2D>();
        _playerRb.GetComponent<PlayerInput>().FireMissile += OnFireMissile;

    }

    private void OnFireMissile(object sender, EventArgs e)
    {
        if(!isAvailable) return;
        
        _audioSource.PlayOneShot(_clipFire, 0.7f);
        _offsetFromPlayer = 0;
        _tr.localPosition = Vector3.zero;
        EnableMissile();

    }


    private void EnableMissile()
    {
        isAvailable = false;
        _rend.enabled = true;
        _rb.simulated = true;
        _offsetFromPlayer = 0;

    }

    private void DisableMissile()
    {
        isAvailable = true;
        _rend.enabled = false;
        _rb.simulated = false;
        _offsetFromPlayer = 0;
    }

    void Update()
    {
        UpdatePosition();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if(!_rb.simulated) return;

        _rb.MovePosition(_pos);
        _offsetFromPlayer += _speed; 
        if(_offsetFromPlayer > _offsetLimit)
            DisableMissile();  
    }

    public void UpdatePosition()
    {
        _pos.x = _playerRb.position.x;
        _pos.y = _playerRb.position.y + _offsetFromPlayer;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        RRactor actor = collision.transform.GetComponent<RRactor>();
        if(actor != null)
        {
            GameManager.Instance.AddScore(actor.Points);
            actor.Despawn();
            GameObject explosion = ExplosionPool.Pool.Get();
            if(explosion != null)
            {     
                explosion.transform.position = collision.transform.position;
                explosion.SetActive(true);              
            }   
        }
        DisableMissile();
    }

    void OnEnable()
    {
    }

    void OnDisable()
    {
//        _playerRb.GetComponent<PlayerInput>().FireMissile -= OnFireMissile;
    }

}
