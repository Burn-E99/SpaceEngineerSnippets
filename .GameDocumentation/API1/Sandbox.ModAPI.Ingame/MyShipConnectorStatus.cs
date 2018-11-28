namespace Sandbox.ModAPI.Ingame
{
	/// <summary>
	/// Describes the current status of the connector.
	/// </summary>
	public enum MyShipConnectorStatus
	{
		/// <summary>
		/// This connector is not connected to anything, nor is it near anything connectable.
		/// </summary>
		Unconnected,
		/// <summary>
		/// This connector is currently near something that it can connect to.
		/// </summary>
		Connectable,
		/// <summary>
		/// This connector is currently connected to something.
		/// </summary>
		Connected
	}
}
