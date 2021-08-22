using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RRGunner : MonoBehaviour
{
    [SerializeField] private float _fireDelay;
    [SerializeField] private AudioClip _clip;
    private RRpatrol _rrPatrol;
    private float _timer;
    private AudioSource _as;
    private Transform _tr;
    private Rigidbody2D _rb;
    private SpriteRenderer _sprend;

    void Awake()
    {
        _rrPatrol = GetComponent<RRpatrol>();
        _tr = transform;
        _rb = GetComponent<Rigidbody2D>();
        _sprend = GetComponent<SpriteRenderer>();
        _as = GameObject.Find("GeneralSFX").GetComponent<AudioSource>();
        _timer = _fireDelay;
    }

    void Update()
    {
        if(!_sprend.isVisible) return;
        
        _timer -= Time.deltaTime;
        if(_timer <= 0)
        {
            _timer = _fireDelay;
            Fire();
        }
    }

    void Fire()
    {
        GameObject go = BulletPool.Pool.Get();
        if(go != null)
        {
            RRBullet bullet = go.GetComponent<RRBullet>();
            go.SetActive(true);
            bullet.Launch(_tr, _tr.localPosition, _rrPatrol.Direction);
            _as.PlayOneShot(_clip, 1.5f);
        }

    }

    void OnEnable()
    {
        _timer = _fireDelay;
    }

}
