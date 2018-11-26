using VRageMath;

namespace Sandbox.ModAPI.Ingame
{
	/// <summary>
	/// Provides basic information about a waypoint.
	/// </summary>
	public struct MyWaypointInfo
	{
		/// <summary>
		/// The waypoint name
		/// </summary>
		public readonly string Name;

		/// <summary>
		/// The coordinates of this waypoint
		/// </summary>
		public readonly Vector3D Coords;

		public MyWaypointInfo(string name, Vector3D coords)
		{
			this.Name = name;
			this.Coords = coords;
		}
	}
}
