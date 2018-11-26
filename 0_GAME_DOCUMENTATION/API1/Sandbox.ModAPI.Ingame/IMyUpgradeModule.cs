using System.Collections.Generic;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ObjectBuilders.Definitions;

namespace Sandbox.ModAPI.Ingame
{
	/// <summary>
	/// Interface to access module upgrades properties
	/// </summary>
	public interface IMyUpgradeModule : IMyFunctionalBlock, IMyTerminalBlock, IMyCubeBlock, IMyEntity
	{
		/// <summary>
		/// Retrieve number of upgrade effects this block has (r/o)
		/// </summary>
		uint UpgradeCount
		{
			get;
		}

		/// <summary>
		/// Retrieve number of blocks this block is connected to (r/o)
		/// </summary>
		uint Connections
		{
			get;
		}

		/// <summary>
		/// Retrieve list of upgrades from this block (r/o), see <see cref="!:Sandbox.Common.ObjectBuilders.Definitions.MyUpgradeModuleInfo">MyUpgradeModuleInfo</see> for details
		/// </summary>
		void GetUpgradeList(out List<MyUpgradeModuleInfo> upgrades);
	}
}
