using Sandbox.ModAPI.Ingame;
using SpaceEngineers.Game.ModAPI.Ingame;
using System;
using VRage.Game.ModAPI.Ingame;

namespace SpaceEngineers.Game.ModAPI
{
	public interface IMyParachute : SpaceEngineers.Game.ModAPI.Ingame.IMyParachute, IMyFunctionalBlock, IMyTerminalBlock, IMyCubeBlock, IMyEntity
	{
		/// <summary>
		/// Event that will trigger true if Chute is now deployed or false if Chute is now cut 
		/// </summary>
		/// <returns></returns>
		event Action<bool> ParachuteStateChanged;

		event Action<bool> DoorStateChanged;
	}
}
