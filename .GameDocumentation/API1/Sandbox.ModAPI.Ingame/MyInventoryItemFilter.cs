using System;
using VRage.Game;

namespace Sandbox.ModAPI.Ingame
{
	[Serializable]
	public struct MyInventoryItemFilter
	{
		/// <summary>
		/// Determines whether all subtypes of the given item ID should pass this filter check.
		/// </summary>
		public readonly bool AllSubTypes;

		/// <summary>
		/// Specifies an item to filter. Set <see cref="F:Sandbox.ModAPI.Ingame.MyInventoryItemFilter.AllSubTypes" /> to true to only check the main type part of this ID.
		/// </summary>
		public readonly MyDefinitionId ItemId;

		public static implicit operator MyInventoryItemFilter(MyDefinitionId definitionId)
		{
			return new MyInventoryItemFilter(definitionId, false);
		}

		public MyInventoryItemFilter(string itemId, bool allSubTypes = false)
		{
			this = default(MyInventoryItemFilter);
			this.ItemId = MyDefinitionId.Parse(itemId);
			this.AllSubTypes = allSubTypes;
		}

		public MyInventoryItemFilter(MyDefinitionId itemId, bool allSubTypes = false)
		{
			this = default(MyInventoryItemFilter);
			this.ItemId = itemId;
			this.AllSubTypes = allSubTypes;
		}
	}
}
