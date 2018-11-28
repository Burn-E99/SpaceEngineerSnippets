using System;
using VRage.Game.ModAPI.Ingame;

namespace Sandbox.ModAPI.Ingame
{
	public interface IMyMechanicalConnectionBlock : IMyFunctionalBlock, IMyTerminalBlock, IMyCubeBlock, IMyEntity
	{
		/// <summary>
		/// Gets the grid of the attached top part
		/// </summary>
		IMyCubeGrid TopGrid
		{
			get;
		}

		/// <summary>
		/// Gets the attached top part entity
		/// </summary>
		IMyAttachableTopBlock Top
		{
			get;
		}

		/// <summary>
		/// Gets or sets the speed at which this device will engage it's safety lock (<see cref="P:Sandbox.ModAPI.Ingame.IMyMechanicalConnectionBlock.IsLocked" />).
		/// </summary>
		[Obsolete("SafetyLock is no longer supported. This is property dummy property only, for backwards compatibility.")]
		float SafetyLockSpeed
		{
			get;
			set;
		}

		/// <summary>
		/// Gets if the block is safety locked (welded)
		/// </summary>
		[Obsolete("SafetyLock is no longer supported. This is property dummy property only, for backwards compatibility.")]
		bool SafetyLock
		{
			get;
			set;
		}

		/// <summary>
		/// Gets if the block base is attached to something
		/// </summary>
		bool IsAttached
		{
			get;
		}

		/// <summary>
		/// Gets if the block is safety locked (welded)
		/// </summary>
		[Obsolete("SafetyLock is no longer supported. This is property dummy property only, for backwards compatibility.")]
		bool IsLocked
		{
			get;
		}

		/// <summary>
		/// Gets if the block is looking for a top part
		/// </summary>
		bool PendingAttachment
		{
			get;
		}

		/// <summary>
		/// Attaches a nearby top part to the block
		/// </summary>
		void Attach();

		/// <summary>
		/// Detaches the top from the base
		/// </summary>
		void Detach();
	}
}
