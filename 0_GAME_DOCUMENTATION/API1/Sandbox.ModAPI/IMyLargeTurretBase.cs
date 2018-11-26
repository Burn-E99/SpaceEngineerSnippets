using Sandbox.ModAPI.Ingame;
using VRage.Game.ModAPI;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Interfaces;
using VRage.ModAPI;

namespace Sandbox.ModAPI
{
	public interface IMyLargeTurretBase : IMyUserControllableGun, IMyFunctionalBlock, IMyTerminalBlock, VRage.Game.ModAPI.IMyCubeBlock, VRage.ModAPI.IMyEntity, Sandbox.ModAPI.Ingame.IMyLargeTurretBase, Sandbox.ModAPI.Ingame.IMyUserControllableGun, Sandbox.ModAPI.Ingame.IMyFunctionalBlock, Sandbox.ModAPI.Ingame.IMyTerminalBlock, VRage.Game.ModAPI.Ingame.IMyCubeBlock, VRage.Game.ModAPI.Ingame.IMyEntity, IMyCameraController
	{
		VRage.ModAPI.IMyEntity Target
		{
			get;
		}

		/// <summary>
		/// Tracks target without position prediction
		/// </summary>
		/// <param name="entity"></param>
		void SetTarget(VRage.ModAPI.IMyEntity entity);

		/// <summary>
		/// Tracks entity with enabled position prediction
		/// </summary>
		/// <param name="entity"></param>
		void TrackTarget(VRage.ModAPI.IMyEntity entity);
	}
}
