using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerFuel))]
public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _clipEngine;
    [SerializeField] private AudioClip _clipRefuel;
    [SerializeField] private AudioClip _clipAlertRefuel;
    [SerializeField] private float _fuelAlertThreshold;

  	private AudioSource _asEngine;
  	private AudioSource _asRefuel;
  	private AudioSource _asFuelAlert;


    void OnEnable()
    {
        PlayerMovement.SpeedChanged += OnSpeedChanged;
        PlayerFuel.FuelChanged += OnFuelChanged;
        PlayerFuel.Refueling += OnRefueling;
        PlayerFuel.RefuelStopped += OnRefuelStopped;

        _asEngine = CreateAudioSource(gameObject, _clipEngine, 0.6f, true);        
        _asRefuel = CreateAudioSource(gameObject, _clipRefuel, 0.7f, false);        
        _asFuelAlert = CreateAudioSource(gameObject, _clipAlertRefuel, 0.4f, false);  
    
    }

    void OnDisable()
    {
        PlayerMovement.SpeedChanged -= OnSpeedChanged;
        PlayerFuel.FuelChanged -= OnFuelChanged;
        PlayerFuel.Refueling -= OnRefueling;
        PlayerFuel.RefuelStopped -= OnRefuelStopped;

        Destroy(_asEngine);
        Destroy(_asRefuel);
        Destroy(_asFuelAlert);
    }

    private AudioSource CreateAudioSource(GameObject go, AudioClip clip, float volume, bool playAwake)
    {
        AudioSource source = go.AddComponent<AudioSource>();
        source.loop = true;
        source.volume = volume;
        source.clip = clip;
        if(playAwake) source.Play();
        return source;
    }

    void OnSpeedChanged(float a)
    {
        _asEngine.pitch = a / 2 + 0.5f;
    }

    void OnFuelChanged(float a)
    {
        if(a < _fuelAlertThreshold)
        {
            if(!_asFuelAlert.isPlaying)
                _asFuelAlert.Play();
        }
        else
            if(_asFuelAlert.isPlaying)
                _asFuelAlert.Stop();

    }

    void OnRefueling()
    {
        if(!_asRefuel.isPlaying)    
            _asRefuel.Play();
    }

    void OnRefuelStopped()
    {
        _asRefuel.Stop();        
    }

}
