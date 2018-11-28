using VRage.Game.ModAPI.Ingame;
using VRageMath;

namespace Sandbox.ModAPI.Ingame
{
	public interface IMyCameraBlock : IMyFunctionalBlock, IMyTerminalBlock, IMyCubeBlock, IMyEntity
	{
		/// <summary>
		/// Determines whether this camera is currently in use.
		/// </summary>
		bool IsActive
		{
			get;
		}

		/// <summary>
		/// The maximum distance that this camera can scan, based on the time since the last scan.
		/// </summary>
		double AvailableScanRange
		{
			get;
		}

		/// <summary>
		/// When this is true, the available raycast distance will count up, and power usage is increased.
		/// </summary>
		bool EnableRaycast
		{
			get;
			set;
		}

		/// <summary>
		/// Returns the maximum positive angle you can apply for pitch and yaw.
		/// </summary>
		float RaycastConeLimit
		{
			get;
		}

		/// <summary>
		/// Returns the maximum distance you can request a raycast. -1 means infinite.
		/// </summary>
		double RaycastDistanceLimit
		{
			get;
		}

		/// <summary>
		/// Does a raycast in the direction the camera is facing. Pitch and Yaw are in degrees. 
		/// Will return an empty struct if distance or angle are out of bounds.
		/// </summary>
		/// <param name="distance"></param>
		/// <param name="pitch"></param>
		/// <param name="yaw"></param>
		/// <returns></returns>
		MyDetectedEntityInfo Raycast(double distance, float pitch = 0f, float yaw = 0f);

		/// <summary>
		/// Does a raycast to the given point. 
		/// Will return an empty struct if distance or angle are out of bounds.
		/// </summary>
		/// <param name="targetPos"></param>
		/// <returns></returns>
		MyDetectedEntityInfo Raycast(Vector3D targetPos);

		/// <summary>
		/// Does a raycast in the given direction. 
		/// Will return an empty struct if distance or angle are out of bounds.
		/// </summary>
		/// <param name="distance"></param>
		/// <param name="targetDirection"></param>
		/// <returns></returns>
		MyDetectedEntityInfo Raycast(double distance, Vector3D targetDirection);

		/// <summary>
		/// Checks if the camera can scan the given distance.
		/// </summary>
		/// <param name="distance"></param>
		/// <returns></returns>
		bool CanScan(double distance);

		/// <summary>
		/// Checks if the camera can scan to the given direction and distance.
		/// </summary>
		/// <param name="distance"></param>
		/// <param name="direction"></param>
		/// <returns></returns>
		bool CanScan(double distance, Vector3D direction);

		/// <summary>
		/// Checks if the camera can scan to the given target
		/// </summary>
		/// <param name="target"></param>
		/// <returns></returns>
		bool CanScan(Vector3D target);

		/// <summary>
		/// Returns the number of milliseconds until the camera can do a raycast of the given distance.
		/// </summary>
		/// <param name="distance"></param>
		/// <returns></returns>
		int TimeUntilScan(double distance);
	}
}
