using System;
using UnityEngine;

public class MissileArgs: EventArgs
{
	public Vector2 Position {get; private set;}
	public float VertSpeed {get; private set;}

	public MissileArgs(Vector2 pos, float vspeed)
	{
		Position = pos;
		VertSpeed = vspeed;
	}
}
