using System;
using System.Collections;
using UnityEngine;

public class StartPauseScreen : MonoBehaviour
{
    [SerializeField] private float _lockDelay = 1f;
    [SerializeField] private bool _locked = true;
    private WaitForSecondsRealtime _waitSeconds;
    private RRPlayerControls _playerControls;
    
    void OnEnable()
    {
        _waitSeconds = new WaitForSecondsRealtime(_lockDelay);
        PlayerMovement.PlayerReset += OnPlayerReset;
        EnableStartPause();
    }

    void OnDisable()
    {
        PlayerMovement.PlayerReset -= OnPlayerReset;
    }

    void Update()
    {
        if(_locked) return;

        int input = (int)(Input.GetAxisRaw("Horizontal") 
                        + Input.GetAxisRaw("Vertical") 
                        + Input.GetAxisRaw("Fire1"));
        if(input != 0) DisableStartPause();
    }

    IEnumerator DelayedUnlock()
    {
        yield return _waitSeconds;
        _locked = false;
    }

    void OnPlayerReset()
    {
        EnableStartPause();
    }

    void EnableStartPause()
    {
        gameObject.SetActive(true);
        AudioListener.pause = true;
        Time.timeScale = 0;   
        _locked = true;
        StartCoroutine(DelayedUnlock());        
    }

    void DisableStartPause()
    {
        AudioListener.pause = false;
        Time.timeScale = 1;   
        gameObject.SetActive(false);       
    }


}
