using Sandbox.ModAPI.Ingame;
using VRage.Game.ModAPI;
using VRage.Game.ModAPI.Ingame;
using VRage.ModAPI;

namespace Sandbox.ModAPI
{
	public interface IMyShipDrill : Sandbox.ModAPI.Ingame.IMyShipDrill, IMyFunctionalBlock, Sandbox.ModAPI.Ingame.IMyFunctionalBlock, IMyTerminalBlock, VRage.Game.ModAPI.IMyCubeBlock, VRage.ModAPI.IMyEntity, Sandbox.ModAPI.Ingame.IMyTerminalBlock, VRage.Game.ModAPI.Ingame.IMyCubeBlock, VRage.Game.ModAPI.Ingame.IMyEntity
	{
		float DrillHarvestMultiplier
		{
			get;
			set;
		}

		float PowerConsumptionMultiplier
		{
			get;
			set;
		}
	}
}
