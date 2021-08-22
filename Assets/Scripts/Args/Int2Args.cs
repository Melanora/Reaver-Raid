using System;
using UnityEngine;

public class Int2Args : EventArgs
{
    public int Value;
    public int Value2;

	public Int2Args()
	{
		Value = 0;
		Value2 = 0;
	}

	public Int2Args(int v, int v2)
	{
		Value = v;
		Value2 = v2;
	}
}
