namespace Sandbox.ModAPI.Ingame
{
	/// <summary>
	/// Describes the current status of the door.
	/// </summary>
	public enum DoorStatus
	{
		/// <summary>
		/// The door is in the process of being opened.
		/// </summary>
		Opening,
		/// <summary>
		/// The door is fully open.
		/// </summary>
		Open,
		/// <summary>
		/// The door is in the process of being closed.
		/// </summary>
		Closing,
		/// <summary>
		/// The door is fully closed.
		/// </summary>
		Closed
	}
}
