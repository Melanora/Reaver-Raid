using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RRExplosion : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;
    private AudioSource _as;

    void Awake()
    {
        _as = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        if(_as == null) return;

        _as.PlayOneShot(_clip);
    }

    void OnBecomeInvisible()
    {
        gameObject.SetActive(false);
    }

}
