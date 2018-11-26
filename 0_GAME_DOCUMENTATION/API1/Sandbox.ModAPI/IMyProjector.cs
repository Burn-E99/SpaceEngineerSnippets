using Sandbox.ModAPI.Ingame;
using VRage.Game;
using VRage.Game.ModAPI;
using VRage.Game.ModAPI.Ingame;
using VRage.ModAPI;

namespace Sandbox.ModAPI
{
	public interface IMyProjector : IMyFunctionalBlock, IMyTerminalBlock, VRage.Game.ModAPI.IMyCubeBlock, VRage.ModAPI.IMyEntity, Sandbox.ModAPI.Ingame.IMyProjector, Sandbox.ModAPI.Ingame.IMyFunctionalBlock, Sandbox.ModAPI.Ingame.IMyTerminalBlock, VRage.Game.ModAPI.Ingame.IMyCubeBlock, VRage.Game.ModAPI.Ingame.IMyEntity
	{
		/// <summary>
		/// The grid currently being projected. Will return null if there is no active projection.
		/// </summary>
		VRage.Game.ModAPI.IMyCubeGrid ProjectedGrid
		{
			get;
		}

		/// <summary>
		/// Allows you to set the currently projected grid
		/// </summary>
		/// <param name="grid"></param>
		void SetProjectedGrid(MyObjectBuilder_CubeGrid grid);

		/// <summary>
		/// Checks if it's possible to build this block.
		/// </summary>
		/// <param name="projectedBlock"></param>
		/// <param name="checkHavokIntersections"></param>
		/// <returns></returns>
		BuildCheckResult CanBuild(VRage.Game.ModAPI.IMySlimBlock projectedBlock, bool checkHavokIntersections);

		/// <summary>
		/// Adds the first component to construction stockpile and creates the block.
		/// This doesn't remove materials from inventory on its own.
		/// </summary>
		/// <param name="cubeBlock"></param>
		/// <param name="owner"></param>
		/// <param name="builder"></param>
		/// <param name="requestInstant"></param>
		void Build(VRage.Game.ModAPI.IMySlimBlock cubeBlock, long owner, long builder, bool requestInstant);
	}
}
