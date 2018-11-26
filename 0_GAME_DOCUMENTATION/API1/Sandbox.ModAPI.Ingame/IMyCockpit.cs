using VRage.Game.ModAPI.Ingame;

namespace Sandbox.ModAPI.Ingame
{
	public interface IMyCockpit : IMyShipController, IMyTerminalBlock, IMyCubeBlock, IMyEntity
	{
		/// <summary>
		/// Determines whether this controller is the main cockpit of the shit this doesn't belong here.
		/// </summary>
		new bool IsMainCockpit
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the maximum oxygen capacity of this cockpit.
		/// </summary>
		float OxygenCapacity
		{
			get;
		}

		/// <summary>
		/// Gets the current oxygen level of this cockpit, as a value between 0 (empty) and 1 (full).
		/// </summary>
		/// <returns></returns>
		float OxygenFilledRatio
		{
			get;
		}
	}
}
