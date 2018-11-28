using Sandbox.ModAPI;
using Sandbox.ModAPI.Ingame;
using SpaceEngineers.Game.ModAPI.Ingame;
using System;
using VRage.Game.ModAPI;
using VRage.Game.ModAPI.Ingame;
using VRage.ModAPI;

namespace SpaceEngineers.Game.ModAPI
{
	public interface IMyLandingGear : Sandbox.ModAPI.IMyFunctionalBlock, Sandbox.ModAPI.IMyTerminalBlock, VRage.Game.ModAPI.IMyCubeBlock, VRage.ModAPI.IMyEntity, SpaceEngineers.Game.ModAPI.Ingame.IMyLandingGear, Sandbox.ModAPI.Ingame.IMyFunctionalBlock, Sandbox.ModAPI.Ingame.IMyTerminalBlock, VRage.Game.ModAPI.Ingame.IMyCubeBlock, VRage.Game.ModAPI.Ingame.IMyEntity
	{
		/// <summary>
		/// Event triggered when the lock mode changes.
		/// </summary>
		event Action<IMyLandingGear, LandingGearMode> LockModeChanged;

		/// <summary>
		/// The lock state changes (locked or unlocked).
		/// </summary>
		[Obsolete("Use LockModeChanged instead.")]
		event Action<bool> StateChanged;

		/// <summary>
		/// Returns the entity the landing gear is attached to.
		/// </summary>
		/// <returns></returns>
		VRage.ModAPI.IMyEntity GetAttachedEntity();
	}
}
