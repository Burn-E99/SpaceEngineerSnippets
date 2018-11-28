using Sandbox.ModAPI.Ingame;
using VRage.Game.ModAPI;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Interfaces;
using VRage.ModAPI;
using VRageMath;

namespace Sandbox.ModAPI
{
	public interface IMyRemoteControl : IMyShipController, IMyTerminalBlock, VRage.Game.ModAPI.IMyCubeBlock, VRage.ModAPI.IMyEntity, IMyControllableEntity, Sandbox.ModAPI.Ingame.IMyRemoteControl, Sandbox.ModAPI.Ingame.IMyShipController, Sandbox.ModAPI.Ingame.IMyTerminalBlock, VRage.Game.ModAPI.Ingame.IMyCubeBlock, VRage.Game.ModAPI.Ingame.IMyEntity
	{
		/// <summary>
		/// Gets the nearest player's position.
		/// </summary>
		/// <param name="playerPosition"></param>
		/// <returns></returns>
		new bool GetNearestPlayer(out Vector3D playerPosition);

		/// <summary>
		/// Gets a destination and tries to fix it so that it does not collide with anything
		/// </summary>
		/// <param name="originalDestination">The final destination that the remote wants to get to.</param>
		/// <param name="checkRadius">The maximum radius until which this method should search.</param>
		/// <param name="shipRadius">The radius of our ship. Make sure that this is large enough to avoid collision. A value of 0f will use the ship's WorldVolume radius.</param>
		/// <returns>Adjusted position</returns>
		Vector3D GetFreeDestination(Vector3D originalDestination, float checkRadius, float shipRadius = 0f);
	}
}
