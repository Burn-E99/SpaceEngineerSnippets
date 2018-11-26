using Sandbox.ModAPI.Ingame;
using VRage.Game.ModAPI;
using VRage.Game.ModAPI.Ingame;
using VRage.ModAPI;

namespace Sandbox.ModAPI
{
	/// <summary>
	/// ModAPI laserantenna block interface
	/// </summary>
	public interface IMyLaserAntenna : IMyFunctionalBlock, IMyTerminalBlock, VRage.Game.ModAPI.IMyCubeBlock, VRage.ModAPI.IMyEntity, Sandbox.ModAPI.Ingame.IMyLaserAntenna, Sandbox.ModAPI.Ingame.IMyFunctionalBlock, Sandbox.ModAPI.Ingame.IMyTerminalBlock, VRage.Game.ModAPI.Ingame.IMyCubeBlock, VRage.Game.ModAPI.Ingame.IMyEntity
	{
		/// <summary>
		/// Gets the antenna on the remote end of the connection.
		/// </summary>
		IMyLaserAntenna Other
		{
			get;
		}

		/// <summary>
		/// Returns <b>true</b> if the specific laser antenna is within connection range.
		/// </summary>
		/// <param name="target"></param>
		/// <returns></returns>
		bool IsInRange(IMyLaserAntenna target);
	}
}
