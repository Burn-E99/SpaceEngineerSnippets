using System;

namespace Sandbox.ModAPI.Ingame
{
	[Flags]
	public enum MyTransmitTarget
	{
		None = 0,
		Owned = 1,
		Ally = 2,
		Neutral = 4,
		Enemy = 8,
		Everyone = 0xF,
		Default = 3
	}
}
