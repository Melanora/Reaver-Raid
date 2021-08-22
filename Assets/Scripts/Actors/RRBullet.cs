using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RRBullet : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] private float _destroyDelay;
    private Rigidbody2D _rb;
    private Transform _tr;
    private GameObject _go;
    private Vector2 _delta = Vector2.zero;
    private float _timer;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _tr = transform;
        _go = gameObject;
        _go.SetActive(false);
    }

    void Update()
    {
        _timer -= Time.deltaTime;
        if(_timer <= 0)
            _go.SetActive(false);
    }

    void OnCollisionEnter2D()
    {
        _go.SetActive(false);
    }

    public void Launch(Transform tr, Vector3 pos, float dir)
    {
        _timer = _destroyDelay;
        _tr.parent = tr.parent;
        pos.z = 0;
        _tr.localPosition = pos;
        _rb.velocity = Vector3.right * dir * _speed;
    }


}
