using System;
using VRage.Game.ModAPI.Ingame;

namespace Sandbox.ModAPI.Ingame
{
	[Obsolete("Use IMyGasGenerator")]
	public interface IMyOxygenGenerator : IMyGasGenerator, IMyFunctionalBlock, IMyTerminalBlock, IMyCubeBlock, IMyEntity
	{
	}
}
