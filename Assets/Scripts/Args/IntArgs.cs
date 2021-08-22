using System;

public class IntArgs: EventArgs
{
    public int Value;

	public IntArgs(int v)
	{

		Value = v;
	}
}
