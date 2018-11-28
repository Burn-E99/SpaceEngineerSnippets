namespace Sandbox.ModAPI.Ingame
{
	/// <summary>
	/// Describes the current status of a laser antenna.
	/// </summary>
	public enum MyLaserAntennaStatus
	{
		/// <summary>
		/// Not doing anything and not connected.
		/// </summary>
		Idle,
		/// <summary>
		/// Currently rotating towards the currently selected target.
		/// </summary>
		RotatingToTarget,
		/// <summary>
		/// Currently searching for a laser antenna at the target.
		/// </summary>
		SearchingTargetForAntenna,
		/// <summary>
		/// Currently connecting to a laser antenna.
		/// </summary>
		Connecting,
		/// <summary>
		/// Currently connected to a laser antenna.
		/// </summary>
		Connected,
		/// <summary>
		/// The target antenna is out of range.
		/// </summary>
		OutOfRange
	}
}
