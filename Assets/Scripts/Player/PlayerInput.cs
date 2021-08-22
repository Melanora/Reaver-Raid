using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour
{
	public event EventHandler FireMissile;
	public event EventHandler<IntArgs> HorizontalInputChanged;

    private PlayerMovement _playerMovement;

    private int _vertical;
    private int _horizontal;
    private int _horizontalLast;
    private IntArgs intArgs;


    void Awake()
    {
		Application.targetFrameRate = 60;
 		Screen.SetResolution(960, 540, false); 

        _playerMovement = GetComponent<PlayerMovement>();
        intArgs = new IntArgs(0);
    }

    void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
		if(Input.GetAxisRaw("Fire1") > 0)
			FireMissile?.Invoke(this, EventArgs.Empty);

		_vertical = (int) Input.GetAxisRaw("Vertical");
        _horizontal = (int) Input.GetAxisRaw("Horizontal");
        intArgs.Value = _horizontal;

        if(_horizontal != _horizontalLast)
            HorizontalInputChanged?.Invoke(this, intArgs);

        _playerMovement.ChangeVelocity(_vertical, _horizontal);
        
        _horizontalLast = _horizontal;
    }
}
