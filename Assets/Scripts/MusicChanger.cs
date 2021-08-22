using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicChanger : MonoBehaviour
{
	public event EventHandler<BoolArgs> OnMuteChange;
    [SerializeField] private bool _allowRepeat;

    private AudioSource _as;
    [SerializeField] private int _tracksTotal = 0;
    private int _trackCurrent;

    void Awake()
    {
        _as = GetComponent<AudioSource>();
        _as.loop = false;

    }

    void Update()
    {
        if(_tracksTotal < 1) return;

        if(Input.GetKeyDown(KeyCode.M))
        {
            _as.mute = !_as.mute;
            if(!_as.mute)
                _as.Stop();
            OnMuteChange?.Invoke(this, new BoolArgs(_as.mute));
        }
        if(!_as.isPlaying) ChangeTrack();
    }

    void ChangeTrack()
    {
        int index = _trackCurrent;
        int i = 10;

        while( (index == _trackCurrent) && (!_allowRepeat) )
        {
            index = (int) Mathf.Floor(UnityEngine.Random.Range(0, _tracksTotal));
            if(++i < 0) break;
        }

        _trackCurrent = index;
        string trackName = "track"+index.ToString();
        Debug.Log("Music: loading track " + trackName);
//        _as.clip = Resources.LoadAsync();
//        _as.Play();
    }


}

