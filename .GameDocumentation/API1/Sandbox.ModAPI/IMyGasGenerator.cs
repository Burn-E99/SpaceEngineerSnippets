using Sandbox.ModAPI.Ingame;
using VRage.Game.ModAPI;
using VRage.Game.ModAPI.Ingame;
using VRage.ModAPI;

namespace Sandbox.ModAPI
{
	/// <summary>
	/// Gas generator interface
	/// </summary>
	public interface IMyGasGenerator : Sandbox.ModAPI.Ingame.IMyGasGenerator, IMyFunctionalBlock, Sandbox.ModAPI.Ingame.IMyFunctionalBlock, IMyTerminalBlock, VRage.Game.ModAPI.IMyCubeBlock, VRage.ModAPI.IMyEntity, Sandbox.ModAPI.Ingame.IMyTerminalBlock, VRage.Game.ModAPI.Ingame.IMyCubeBlock, VRage.Game.ModAPI.Ingame.IMyEntity
	{
		/// <summary>
		/// Increase/decrese O2 produced
		/// </summary>
		float ProductionCapacityMultiplier
		{
			get;
			set;
		}

		/// <summary>
		/// Increase/decrese power consumption
		/// </summary>
		float PowerConsumptionMultiplier
		{
			get;
			set;
		}
	}
}
