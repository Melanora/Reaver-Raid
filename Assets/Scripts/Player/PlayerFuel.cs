using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerFuel : MonoBehaviour
{

	[SerializeField] private float _fuelLossRate;
	[SerializeField] private float _refuelRate;
	[SerializeField] private float _fuelCurrent;
	[SerializeField] private float _fuelMax;

	[SerializeField] private AudioClip _clipRefuel;
	[SerializeField] private AudioClip _clipNoFuel;

  	public static event UnityAction NoFuel;
  	public static event UnityAction Refueling;
  	public static event UnityAction RefuelStopped;
	public static event UnityAction<float> FuelChanged;

	private bool _isRefueling;


    void OnEnable()
    {
		NoFuel += OnNoFuel;
    }

    void OnDisable()
    {
		NoFuel -= OnNoFuel;
    }

    void Update()
    {
        UseFuel();
    }

    private void UseFuel()
    {
		if(_isRefueling) return;

		if(_fuelCurrent > 0)
	        ModifyFuel(-_fuelLossRate * Time.deltaTime);
		else
		{
	        ModifyFuel(_fuelMax);
            NoFuel?.Invoke();
		}
    }

	void Refuel()
	{
		_isRefueling = true;
//		_asRefuel.pitch = (_fuelCurrent < _fuelMax)? 0.8f: 1f;
		if(_fuelCurrent < _fuelMax) 
			ModifyFuel(_refuelRate * Time.deltaTime);

//		if(!_asRefuel.isPlaying) _asRefuel.Play();
	}

    void ModifyFuel(float f)
    {
		_fuelCurrent += f;
		FuelChanged?.Invoke(_fuelCurrent / _fuelMax);
    }

	void OnTriggerStay2D(Collider2D trigger)
	{
		Refuel();	
		Refueling?.Invoke();
	}
	
	void OnTriggerExit2D(Collider2D trigger)
	{
		_isRefueling = false;
		RefuelStopped?.Invoke();

//		if(_asRefuel.isPlaying) _asRefuel.Stop();
	}

	void OnNoFuel()
	{
		_fuelCurrent = _fuelMax;
	}
}
