using System;

public class PlayerMovementArgs: EventArgs
{
    public PlayerMovement Value;

	public PlayerMovementArgs(PlayerMovement pm)
	{
		Value = pm;
	}
}
