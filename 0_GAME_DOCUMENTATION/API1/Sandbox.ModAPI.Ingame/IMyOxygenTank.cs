using System;
using VRage.Game.ModAPI.Ingame;

namespace Sandbox.ModAPI.Ingame
{
	[Obsolete("Use IMyGasTank")]
	public interface IMyOxygenTank : IMyGasTank, IMyFunctionalBlock, IMyTerminalBlock, IMyCubeBlock, IMyEntity
	{
		/// <summary>
		/// Gets the current oxygen level of this tank, as a value between 0 (empty) and 1 (full).
		/// </summary>
		/// <returns></returns>
		[Obsolete("Use IMyGasTank.FilledRatio")]
		double GetOxygenLevel();
	}
}
