using Sandbox.ModAPI.Ingame;
using System;
using VRage.Game.ModAPI.Ingame;

namespace SpaceEngineers.Game.ModAPI.Ingame
{
	public interface IMyLandingGear : IMyFunctionalBlock, IMyTerminalBlock, IMyCubeBlock, IMyEntity
	{
		/// <summary>
		/// Gets if the landing gear lock can be broken with force.
		/// </summary>
		[Obsolete("Landing gear are not breakable anymore.")]
		bool IsBreakable
		{
			get;
		}

		/// <summary>
		/// Gets whether the landing gear is currently locked.
		/// </summary>
		bool IsLocked
		{
			get;
		}

		/// <summary>
		/// Toggles the autolock of the landing gear.
		/// </summary>
		bool AutoLock
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the current lock state of the landing gear.
		/// </summary>
		LandingGearMode LockMode
		{
			get;
		}

		/// <summary>
		/// Locks the landing gear.
		/// </summary>
		void Lock();

		/// <summary>
		/// Toggles the landing gear lock.
		/// </summary>
		void ToggleLock();

		/// <summary>
		/// Unlocks the landing gear.
		/// </summary>
		void Unlock();

		/// <summary>
		/// Resets autolock timer
		/// </summary>
		void ResetAutoLock();
	}
}
