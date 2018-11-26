using System;

namespace Sandbox.ModAPI.Ingame
{
	/// <summary>
	/// Flags set how often the script will run itself.
	/// </summary>
	[Flags]
	public enum UpdateFrequency : byte
	{
		/// <summary>
		/// Does not run autonomously.
		/// </summary>
		None = 0,
		/// <summary>
		/// Run every game tick.
		/// </summary>
		Update1 = 1,
		/// <summary>
		/// Run every 10th game tick.
		/// </summary>
		Update10 = 2,
		/// <summary>
		/// Run every 100th game tick.
		/// </summary>
		Update100 = 4,
		/// <summary>
		/// Run once before the next tick. Flag is un-set automatically after the update
		/// </summary>
		Once = 8
	}
}
