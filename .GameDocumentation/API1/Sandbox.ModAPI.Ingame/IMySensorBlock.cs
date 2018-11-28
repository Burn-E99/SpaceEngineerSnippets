using System.Collections.Generic;
using VRage.Game.ModAPI.Ingame;

namespace Sandbox.ModAPI.Ingame
{
	public interface IMySensorBlock : IMyFunctionalBlock, IMyTerminalBlock, IMyCubeBlock, IMyEntity
	{
		/// <summary>
		/// Gets the maximum range of the sensor in any direction.
		/// </summary>
		float MaxRange
		{
			get;
		}

		/// <summary>
		/// Gets or sets the left range of the sensor.
		/// </summary>
		float LeftExtend
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the right range of the sensor.
		/// </summary>
		float RightExtend
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the top range of the sensor.
		/// </summary>
		float TopExtend
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the bottom range of the sensor.
		/// </summary>
		float BottomExtend
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the front range of the sensor.
		/// </summary>
		float FrontExtend
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the back range of the sensor.
		/// </summary>
		float BackExtend
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets if the proximity sound plays when an entity is detected.
		/// </summary>
		bool PlayProximitySound
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets if the sensor should detect players.
		/// </summary>
		bool DetectPlayers
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets if the sensor should detect floating objects (components, rocks).
		/// </summary>
		bool DetectFloatingObjects
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets if the sensor should detect small ships.
		/// </summary>
		bool DetectSmallShips
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets if the sensor should detect large ships.
		/// </summary>
		bool DetectLargeShips
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets if the sensor should detect large stations.
		/// </summary>
		bool DetectStations
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets if the sensor should detect subgrids (eg. connected by connector).
		/// </summary>
		bool DetectSubgrids
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets if the sensor should detect asteroids or planets.
		/// </summary>
		bool DetectAsteroids
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets if the sensor should detect the block owner.
		/// </summary>
		/// <remarks>Requires DetectPlayers set to <b>true</b>.</remarks>
		bool DetectOwner
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets if the sensor should detect friendly players.
		/// </summary>
		/// <remarks>Requires DetectPlayers set to <b>true</b>.</remarks>
		bool DetectFriendly
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets if the sensor should detect neutral players.
		/// </summary>
		/// <remarks>Requires DetectPlayers set to <b>true</b>.</remarks>
		bool DetectNeutral
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets if the sensor should detect enemy players.
		/// </summary>
		/// <remarks>Requires DetectPlayers set to <b>true</b>.</remarks>
		bool DetectEnemy
		{
			get;
			set;
		}

		/// <summary>
		/// Gets if there is any entity currently being detected.
		/// </summary>
		bool IsActive
		{
			get;
		}

		MyDetectedEntityInfo LastDetectedEntity
		{
			get;
		}

		void DetectedEntities(List<MyDetectedEntityInfo> entities);
	}
}
