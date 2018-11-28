using System.Collections.Generic;
using VRage.Game.ModAPI.Ingame;
using VRageMath;

namespace Sandbox.ModAPI.Ingame
{
	public interface IMyRemoteControl : IMyShipController, IMyTerminalBlock, IMyCubeBlock, IMyEntity
	{
		/// <summary>
		/// Determines whether the autopilot is currently enabled.
		/// </summary>
		bool IsAutoPilotEnabled
		{
			get;
		}

		/// <summary>
		/// Gets or sets the autopilot speed limit
		/// </summary>
		float SpeedLimit
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the current flight mode
		/// </summary>
		FlightMode FlightMode
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the current flight direction
		/// </summary>
		Base6Directions.Direction Direction
		{
			get;
			set;
		}

		MyWaypointInfo CurrentWaypoint
		{
			get;
		}

		/// <summary>
		/// Gets the nearest player's position. Will only work if the remote control belongs to an NPC
		/// </summary>
		/// <param name="playerPosition"></param>
		/// <returns></returns>
		bool GetNearestPlayer(out Vector3D playerPosition);

		/// <summary>
		/// Removes all existing waypoints.
		/// </summary>
		void ClearWaypoints();

		/// <summary>
		/// Gets basic information about the currently configured waypoints.
		/// </summary>
		/// <param name="waypoints"></param>
		void GetWaypointInfo(List<MyWaypointInfo> waypoints);

		/// <summary>
		/// Adds a new waypoint.
		/// </summary>
		/// <param name="coords"></param>
		/// <param name="name"></param>
		void AddWaypoint(Vector3D coords, string name);

		/// <summary>
		/// Enables or disables the autopilot.
		/// </summary>
		/// <param name="enabled"></param>
		void SetAutoPilotEnabled(bool enabled);

		/// <summary>
		/// Enables or disables collision avoidance.
		/// </summary>
		/// <param name="enabled"></param>
		void SetCollisionAvoidance(bool enabled);

		/// <summary>
		/// Enables or disables docking mode.
		/// </summary>
		/// <param name="enabled"></param>
		void SetDockingMode(bool enabled);
	}
}
