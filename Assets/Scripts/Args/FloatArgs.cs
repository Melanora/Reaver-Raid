using System;
using UnityEngine;

public class FloatArgs: EventArgs
{
    public float Value;

	public FloatArgs()
	{
		Value = 0;
	}
	public FloatArgs(float v)
	{
		Value = v;
	}
}
