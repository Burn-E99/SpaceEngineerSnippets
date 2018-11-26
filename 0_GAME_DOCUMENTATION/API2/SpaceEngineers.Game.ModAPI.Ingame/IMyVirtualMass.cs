using Sandbox.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame;

namespace SpaceEngineers.Game.ModAPI.Ingame
{
	public interface IMyVirtualMass : IMyFunctionalBlock, IMyTerminalBlock, IMyCubeBlock, IMyEntity
	{
		/// <summary>
		/// Virtualmass weight
		/// </summary>
		float VirtualMass
		{
			get;
		}
	}
}
