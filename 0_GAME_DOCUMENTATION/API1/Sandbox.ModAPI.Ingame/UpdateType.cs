using System;

namespace Sandbox.ModAPI.Ingame
{
	/// <summary>
	/// Enum describes what source triggered the script to run.
	/// </summary>
	[Flags]
	public enum UpdateType
	{
		None = 0,
		/// <summary>
		/// Script run by user in the terminal.
		/// </summary>
		Terminal = 1,
		/// <summary>
		/// Script run by a block such as timer, sensor.
		/// </summary>
		Trigger = 2,
		/// <summary>
		/// Script run by antenna receiving a message.
		/// </summary>
		Antenna = 4,
		/// <summary>
		/// Script run by a mod.
		/// </summary>
		Mod = 8,
		/// <summary>
		/// Script run by another programmable block.
		/// </summary>
		Script = 0x10,
		/// <summary>
		/// Script is updating every tick.
		/// </summary>
		Update1 = 0x20,
		/// <summary>
		/// Script is updating every 10th tick.
		/// </summary>
		Update10 = 0x40,
		/// <summary>
		/// Script is updating every 100th tick.
		/// </summary>
		Update100 = 0x80,
		/// <summary>
		/// Script is updating once before the tick.
		/// </summary>
		Once = 0x100
	}
}
