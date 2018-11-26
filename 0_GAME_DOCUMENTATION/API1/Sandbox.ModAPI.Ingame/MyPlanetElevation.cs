namespace Sandbox.ModAPI.Ingame
{
	/// <summary>
	/// Describes what detail level to retrieve the planet elevation for.
	/// </summary>
	public enum MyPlanetElevation
	{
		/// <summary>
		/// Only return the distance to the planetary sealevel.
		/// </summary>
		Sealevel,
		/// <summary>
		/// Return the distance to the closest point of the planet. This is the same value
		/// displayed in the HUD.
		/// </summary>
		Surface
	}
}
