using Sandbox.ModAPI.Ingame;
using System;
using System.Text;
using VRage.Game.ModAPI;
using VRage.Game.ModAPI.Ingame;
using VRage.ModAPI;

namespace Sandbox.ModAPI
{
	public interface IMyTerminalBlock : VRage.Game.ModAPI.IMyCubeBlock, VRage.ModAPI.IMyEntity, Sandbox.ModAPI.Ingame.IMyTerminalBlock, VRage.Game.ModAPI.Ingame.IMyCubeBlock, VRage.Game.ModAPI.Ingame.IMyEntity
	{
		event Action<IMyTerminalBlock> CustomDataChanged;

		event Action<IMyTerminalBlock> CustomNameChanged;

		event Action<IMyTerminalBlock> OwnershipChanged;

		event Action<IMyTerminalBlock> PropertiesChanged;

		event Action<IMyTerminalBlock> ShowOnHUDChanged;

		event Action<IMyTerminalBlock> VisibilityChanged;

		/// <summary>
		/// Event to append custom info.
		/// </summary>
		event Action<IMyTerminalBlock, StringBuilder> AppendingCustomInfo;

		/// <summary>
		/// Raises AppendingCustomInfo so every subscriber can append custom info.
		/// </summary>
		void RefreshCustomInfo();
	}
}
