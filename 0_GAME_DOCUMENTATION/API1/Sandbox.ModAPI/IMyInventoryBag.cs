using VRage.Game.ModAPI.Ingame;
using VRage.ModAPI;

namespace Sandbox.ModAPI
{
	/// <summary>
	/// Inventory bag spawned when character died, container breaks, or when entity from other inventory cannot be spawned then bag spawned with the item in its inventory.
	/// </summary>
	public interface IMyInventoryBag : VRage.ModAPI.IMyEntity, VRage.Game.ModAPI.Ingame.IMyEntity
	{
	}
}
