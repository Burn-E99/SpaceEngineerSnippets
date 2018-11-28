using Sandbox.ModAPI.Ingame;
using System;
using VRage.Game.ModAPI;
using VRage.Game.ModAPI.Ingame;
using VRage.ModAPI;

namespace Sandbox.ModAPI
{
	public interface IMyThrust : Sandbox.ModAPI.Ingame.IMyThrust, IMyFunctionalBlock, Sandbox.ModAPI.Ingame.IMyFunctionalBlock, IMyTerminalBlock, VRage.Game.ModAPI.IMyCubeBlock, VRage.ModAPI.IMyEntity, Sandbox.ModAPI.Ingame.IMyTerminalBlock, VRage.Game.ModAPI.Ingame.IMyCubeBlock, VRage.Game.ModAPI.Ingame.IMyEntity
	{
		float ThrustMultiplier
		{
			get;
			set;
		}

		float PowerConsumptionMultiplier
		{
			get;
			set;
		}

		event Action<IMyThrust, float> ThrustOverrideChanged;
	}
}
