using System;

public class BoolArgs: EventArgs
{
    public bool Value;

	public BoolArgs(bool v)
	{
		Value = v;
	}
}
