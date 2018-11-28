namespace Sandbox.ModAPI.Ingame
{
	/// <summary>
	/// Determines the current mode of a conveyor sorter.
	/// </summary>
	public enum MyConveyorSorterMode
	{
		/// <summary>
		/// The items in the filter list are the only items allowed through this sorter.
		/// </summary>
		Whitelist,
		/// <summary>
		/// The items in the filter list are not allowed through this sorter.
		/// </summary>
		Blacklist
	}
}
