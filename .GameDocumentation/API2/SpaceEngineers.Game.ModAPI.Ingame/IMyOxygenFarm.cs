using Sandbox.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame;

namespace SpaceEngineers.Game.ModAPI.Ingame
{
	public interface IMyOxygenFarm : IMyTerminalBlock, IMyCubeBlock, IMyEntity
	{
		/// <summary>
		/// Gets if the block can produce gas.
		/// </summary>
		bool CanProduce
		{
			get;
		}

		float GetOutput();
	}
}
