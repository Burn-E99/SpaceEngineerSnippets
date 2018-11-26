using Sandbox.ModAPI.Ingame;
using VRage.Game.ModAPI;
using VRage.Game.ModAPI.Ingame;
using VRage.ModAPI;

namespace Sandbox.ModAPI
{
	public interface IMyGyro : Sandbox.ModAPI.Ingame.IMyGyro, IMyFunctionalBlock, Sandbox.ModAPI.Ingame.IMyFunctionalBlock, IMyTerminalBlock, VRage.Game.ModAPI.IMyCubeBlock, VRage.ModAPI.IMyEntity, Sandbox.ModAPI.Ingame.IMyTerminalBlock, VRage.Game.ModAPI.Ingame.IMyCubeBlock, VRage.Game.ModAPI.Ingame.IMyEntity
	{
		float GyroStrengthMultiplier
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
