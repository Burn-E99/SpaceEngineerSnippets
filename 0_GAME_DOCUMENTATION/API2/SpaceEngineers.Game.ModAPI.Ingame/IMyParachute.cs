using Sandbox.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame;
using VRageMath;

namespace SpaceEngineers.Game.ModAPI.Ingame
{
	public interface IMyParachute : IMyFunctionalBlock, IMyTerminalBlock, IMyCubeBlock, IMyEntity
	{
		/// <summary>
		/// Atmospheric Density at the block. Returns 0.0f if not near a planet. 
		/// </summary>
		float Atmosphere
		{
			get;
		}

		/// <summary>
		/// Determines the current general status of the door.
		/// </summary>
		DoorStatus Status
		{
			get;
		}

		/// <summary>
		/// The current, accurate ratio of the door's current state where 0 is fully closed and 1 is fully open.
		/// </summary>
		float OpenRatio
		{
			get;
		}

		/// <summary>
		/// Gets Elevation, elevation will equal positive infinity if method returns false
		/// </summary>
		/// <param name="detail">Describes what detail level to retrieve the planet elevation</param>
		/// <param name="elevation">Elevation in meters</param>
		/// <returns></returns>
		bool TryGetClosestPoint(out Vector3D? closestPoint);

		/// <summary>
		/// Determines the linear velocities in meters per second at the block position
		/// </summary>
		Vector3D GetVelocity();

		/// <summary>
		/// Gets the detected natural gravity vector and power at the current location.
		/// </summary>
		/// <returns></returns>
		Vector3D GetNaturalGravity();

		/// <summary>
		/// Gets the detected artificial gravity vector and power at the current location.
		/// </summary>
		/// <returns></returns>
		Vector3D GetArtificialGravity();

		/// <summary>
		/// Gets the total accumulated gravity vector and power at the current location, 
		/// taking both natural and artificial gravity into account.
		/// </summary>
		/// <returns></returns>
		Vector3D GetTotalGravity();

		/// <summary>
		/// Opens the door. See <see cref="P:SpaceEngineers.Game.ModAPI.Ingame.IMyParachute.Status" /> to get the current status.
		/// </summary>
		void OpenDoor();

		/// <summary>
		/// Closes the door. See <see cref="P:SpaceEngineers.Game.ModAPI.Ingame.IMyParachute.Status" /> to get the current status.
		/// </summary>
		void CloseDoor();

		/// <summary>
		/// Toggles the open state of this door. See <see cref="P:SpaceEngineers.Game.ModAPI.Ingame.IMyParachute.Status" /> to get the current status.
		/// </summary>
		void ToggleDoor();
	}
}
