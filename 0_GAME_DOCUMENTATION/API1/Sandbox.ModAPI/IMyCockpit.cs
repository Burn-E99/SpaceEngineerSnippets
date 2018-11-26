using Sandbox.ModAPI.Ingame;
using VRage.Game.ModAPI;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Interfaces;
using VRage.ModAPI;

namespace Sandbox.ModAPI
{
	public interface IMyCockpit : IMyShipController, IMyTerminalBlock, VRage.Game.ModAPI.IMyCubeBlock, VRage.ModAPI.IMyEntity, IMyControllableEntity, Sandbox.ModAPI.Ingame.IMyCockpit, Sandbox.ModAPI.Ingame.IMyShipController, Sandbox.ModAPI.Ingame.IMyTerminalBlock, VRage.Game.ModAPI.Ingame.IMyCubeBlock, VRage.Game.ModAPI.Ingame.IMyEntity, IMyCameraController
	{
		/// <summary>
		/// Gets or sets the current oxygen level of this cockpit, as a value between 0 (empty) and 1 (full).
		/// </summary>
		new float OxygenFilledRatio
		{
			get;
			set;
		}

		/// <summary>
		/// Place a pilot in the cockpit seat.
		/// </summary>
		/// <param name="pilot">Character to place in seat</param>
		void AttachPilot(IMyCharacter pilot);

		/// <summary>
		/// Removes an attached pilot. Call on server.
		/// </summary>
		void RemovePilot();
	}
}
