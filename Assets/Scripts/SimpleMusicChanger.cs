using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class SimpleMusicChanger : MonoBehaviour
{
    [SerializeField] private bool _allowRepeat;
    [SerializeField] private AudioClip[] _tracks;
	public event UnityAction<bool> OnMuteChange;

    private AudioSource _as;
    private int _tracksTotal;
    private int _trackCurrent;

    void Awake()
    {
        _as = GetComponent<AudioSource>();
        _as.loop = false;
        _tracksTotal = _tracks.Length;
        _trackCurrent = 0;

    }

    void Update()
    {
        if(_tracksTotal < 1 || Time.timeScale < 1) return;

        if(Input.GetKeyDown(KeyCode.M))
        {
            _as.mute = !_as.mute;
            if(!_as.mute)
                _as.Stop();
            OnMuteChange?.Invoke(_as.mute);
        }
        if(!_as.isPlaying) ChangeTrack();
    }

    void ChangeTrack()
    {
        int index = _trackCurrent;

        while( (index == _trackCurrent) && (!_allowRepeat) )
            index = (int)UnityEngine.Random.Range(0, _tracksTotal);

        _trackCurrent = index;
        _as.clip = _tracks[index];
        _as.Play();
    }


}

