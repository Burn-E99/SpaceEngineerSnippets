using System;
using VRage.Game.ModAPI.Ingame;

namespace Sandbox.ModAPI.Ingame
{
	public interface IMyDoor : IMyFunctionalBlock, IMyTerminalBlock, IMyCubeBlock, IMyEntity
	{
		/// <summary>
		/// Indicates whether door is opened or closed. True when door is opened.
		/// </summary>
		[Obsolete("Use the Status property instead")]
		bool Open
		{
			get;
		}

		/// <summary>
		/// Determines the current general status of the door.
		/// </summary>
		DoorStatus Status
		{
			get;
		}

		/// <summary>
		/// The current, accurate ratio of the door's current state where 0 is fully closed and 1 is fully open.
		/// </summary>
		float OpenRatio
		{
			get;
		}

		/// <summary>
		/// Opens the door. See <see cref="P:Sandbox.ModAPI.Ingame.IMyDoor.Status" /> to get the current status.
		/// </summary>
		void OpenDoor();

		/// <summary>
		/// Closes the door. See <see cref="P:Sandbox.ModAPI.Ingame.IMyDoor.Status" /> to get the current status.
		/// </summary>
		void CloseDoor();

		/// <summary>
		/// Toggles the open state of this door. See <see cref="P:Sandbox.ModAPI.Ingame.IMyDoor.Status" /> to get the current status.
		/// </summary>
		void ToggleDoor();
	}
}
