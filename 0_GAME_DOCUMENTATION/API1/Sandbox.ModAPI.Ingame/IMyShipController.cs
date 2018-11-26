using VRage.Game.ModAPI.Ingame;
using VRageMath;

namespace Sandbox.ModAPI.Ingame
{
	public interface IMyShipController : IMyTerminalBlock, IMyCubeBlock, IMyEntity
	{
		/// <summary>
		/// Determines whether this specific ship controller is capable of controlling the ship it's installed on.
		/// </summary>
		bool CanControlShip
		{
			get;
		}

		/// <summary>
		/// Indicates whether a block is locally or remotely controlled.
		/// </summary>
		bool IsUnderControl
		{
			get;
		}

		/// <summary>
		/// Determines whether there are any wheels on this ship.
		/// </summary>
		bool HasWheels
		{
			get;
		}

		/// <summary>
		/// Gets or sets whether wheels are being controlled by this controller.
		/// </summary>
		bool ControlWheels
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets whether thrusters are being controlled by this controller.
		/// </summary>
		bool ControlThrusters
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the current state of the handbrake.
		/// </summary>
		bool HandBrake
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets whether dampeners are currently enabled.
		/// </summary>
		bool DampenersOverride
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets whether the horizon indicator should be displayed for this block.
		/// </summary>
		bool ShowHorizonIndicator
		{
			get;
			set;
		}

		/// <summary>
		/// Directional input from user/autopilot. Values can be very large with high controller sensitivity
		/// </summary>
		Vector3 MoveIndicator
		{
			get;
		}

		/// <summary>
		/// Pitch, yaw input from user/autopilot. Values can be very large with high controller sensitivity
		/// </summary>
		Vector2 RotationIndicator
		{
			get;
		}

		/// <summary>
		/// Roll input from user/autopilot. Values can be very large with high controller sensitivity
		/// </summary>
		float RollIndicator
		{
			get;
		}

		/// <summary>
		/// Center of mass in world coordinates
		/// </summary>
		Vector3D CenterOfMass
		{
			get;
		}

		/// <summary>
		/// Gets or sets if this controller is the main one.
		/// </summary>
		bool IsMainCockpit
		{
			get;
			set;
		}

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
		/// Gets the basic ship speed in meters per second, for when you just need to know how fast you're going.
		/// </summary>
		/// <returns></returns>
		double GetShipSpeed();

		/// <summary>
		/// Determines the linear velocities in meters per second and angular velocities in radians per second. 
		/// Provides a more accurate representation of the directions and axis speeds.
		/// </summary>
		MyShipVelocities GetShipVelocities();

		/// <summary>
		/// Gets information about the current mass of the ship.
		/// </summary>
		/// <returns></returns>
		MyShipMass CalculateShipMass();

		/// <summary>
		/// Attempts to get the world position of the nearest planet. This method is only available when a ship is 
		/// within the gravity well of a planet.
		/// </summary>
		/// <param name="position"></param>
		/// <returns></returns>
		bool TryGetPlanetPosition(out Vector3D position);

		/// <summary>
		/// Attempts to get the elevation of the ship in relation to the nearest planet. This method is only available
		/// when a ship is within the gravity well of a planet.
		/// </summary>
		/// <param name="detail"></param>
		/// <param name="elevation"></param>
		/// <returns></returns>
		bool TryGetPlanetElevation(MyPlanetElevation detail, out double elevation);
	}
}
