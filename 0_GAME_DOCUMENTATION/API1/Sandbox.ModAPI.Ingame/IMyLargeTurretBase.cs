using VRage.Game.ModAPI.Ingame;
using VRageMath;

namespace Sandbox.ModAPI.Ingame
{
	public interface IMyLargeTurretBase : IMyUserControllableGun, IMyFunctionalBlock, IMyTerminalBlock, IMyCubeBlock, IMyEntity
	{
		/// <summary>
		/// Indicates whether a block is locally or remotely controlled.
		/// </summary>
		bool IsUnderControl
		{
			get;
		}

		bool CanControl
		{
			get;
		}

		float Range
		{
			get;
		}

		bool IsAimed
		{
			get;
		}

		/// <summary>
		/// Checks if the turret is locked onto a target
		/// </summary>
		bool HasTarget
		{
			get;
		}

		/// <summary>
		/// Sets/gets elevation of turret, this method is not synced, you need to sync elevation manually
		/// </summary>
		float Elevation
		{
			get;
			set;
		}

		/// <summary>
		/// Sets/gets azimuth of turret, this method is not synced, you need to sync azimuth manually
		/// </summary>
		float Azimuth
		{
			get;
			set;
		}

		/// <summary>
		/// enable/disable idle rotation for turret, this method is not synced, you need to sync manually
		/// </summary>
		bool EnableIdleRotation
		{
			get;
			set;
		}

		/// <summary>
		/// Checks is AI is enabled for turret
		/// </summary>
		bool AIEnabled
		{
			get;
		}

		/// <summary>
		/// Tracks given target with enabled position prediction
		/// </summary>
		/// <param name="pos"></param>
		/// <param name="velocity"></param>
		void TrackTarget(Vector3D pos, Vector3 velocity);

		/// <summary>
		/// Targets given position
		/// </summary>
		/// <param name="pos"></param>
		void SetTarget(Vector3D pos);

		/// <summary>
		/// method used to sync elevation of turret , you need to call it to sync elevation for other clients/server
		/// </summary>
		void SyncElevation();

		/// <summary>
		/// method used to sync azimuth, you need to call it to sync azimuth for other clients/server
		/// </summary>
		void SyncAzimuth();

		/// <summary>
		/// method used to sync idle rotation and elevation, you need to call it to sync rotation and elevation for other clients/server
		/// </summary>
		void SyncEnableIdleRotation();

		/// <summary>
		/// resert targeting to default values
		/// </summary>
		void ResetTargetingToDefault();

		/// <summary>
		/// Gets the turret's current detected entity, if any
		/// </summary>
		/// <returns></returns>
		MyDetectedEntityInfo GetTargetedEntity();
	}
}
