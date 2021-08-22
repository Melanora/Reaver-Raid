using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStest : MonoBehaviour
{
    [SerializeField] private AudioClip[] _clips;
    [SerializeField] private bool _stop;
    [SerializeField] private bool _isPlaying;
    private AudioSource _as;
    private AudioSource _as2;
    private float _timer = 0;
    private float _delay;

    void Start()
    {
        _as = gameObject.AddComponent<AudioSource>(); // GetComponent<AudioSource>();
        _as.loop = true;
        _as.volume = 0.2f;
        _as.bypassEffects = true;
        _as.bypassListenerEffects = true;
        _as.bypassReverbZones = true;
        _as.clip = _clips[0];
        _as.Play();

        _as2 = gameObject.AddComponent<AudioSource>(); // GetComponent<AudioSource>();
        _as2.loop = true;
        _as2.volume = 0.2f;
        _as2.bypassEffects = true;
        _as2.bypassListenerEffects = true;
        _as2.bypassReverbZones = true;
        _as2.clip = _clips[1];
        _as2.Play();

        _timer = _delay;
    }

    void Update()
    {
        if(_as == null) return;

        if(--_timer <= 0)
        {
            _delay = Random.Range(1, 5);
            _timer = _delay;
            PlayRandomClip();
        }
        _isPlaying = _as.isPlaying;
        if(_stop) _as.Stop();
    }

    void PlayRandomClip()
    {
        int index = (int)Random.Range(0, _clips.Length-1);
        _as.PlayOneShot(_clips[index]);
    }

}
