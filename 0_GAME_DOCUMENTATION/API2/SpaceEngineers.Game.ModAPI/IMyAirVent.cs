using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI;
using Sandbox.ModAPI.Ingame;
using SpaceEngineers.Game.ModAPI.Ingame;
using VRage.Game.Components;
using VRage.Game.ModAPI;
using VRage.Game.ModAPI.Ingame;
using VRage.ModAPI;

namespace SpaceEngineers.Game.ModAPI
{
	public interface IMyAirVent : Sandbox.ModAPI.IMyFunctionalBlock, Sandbox.ModAPI.IMyTerminalBlock, VRage.Game.ModAPI.IMyCubeBlock, VRage.ModAPI.IMyEntity, SpaceEngineers.Game.ModAPI.Ingame.IMyAirVent, Sandbox.ModAPI.Ingame.IMyFunctionalBlock, Sandbox.ModAPI.Ingame.IMyTerminalBlock, VRage.Game.ModAPI.Ingame.IMyCubeBlock, VRage.Game.ModAPI.Ingame.IMyEntity
	{
		/// <summary>
		/// How much gas can be pushed out per second
		/// </summary>
		float GasOutputPerSecond
		{
			get;
		}

		/// <summary>
		/// How much gas can be pulled in per second
		/// </summary>
		float GasInputPerSecond
		{
			get;
		}

		/// <summary>
		/// Resource sink component for gas
		/// </summary>
		MyResourceSinkInfo OxygenSinkInfo
		{
			get;
			set;
		}

		/// <summary>
		/// Resource source component
		/// </summary>
		MyResourceSourceComponent SourceComp
		{
			get;
			set;
		}
	}
}
