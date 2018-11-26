using System;
using System.Collections.Generic;
using VRage.Game.Entity;
using VRage.Game.ModAPI.Ingame;

namespace Sandbox.ModAPI.Ingame
{
	public static class TerminalBlockExtentions
	{
		public static long GetId(this IMyTerminalBlock block)
		{
			return block.EntityId;
		}

		public static void ApplyAction(this IMyTerminalBlock block, string actionName)
		{
			block.GetActionWithName(actionName).Apply(block);
		}

		public static void ApplyAction(this IMyTerminalBlock block, string actionName, List<TerminalActionParameter> parameters)
		{
			block.GetActionWithName(actionName).Apply(block, parameters);
		}

		public static bool HasAction(this IMyTerminalBlock block, string actionName)
		{
			return block.GetActionWithName(actionName) != null;
		}

		[Obsolete("Use the HasInventory property.")]
		public static bool HasInventory(this IMyTerminalBlock block)
		{
			MyEntity myEntity = block as MyEntity;
			if (myEntity == null)
			{
				return false;
			}
			if (!(block is IMyInventoryOwner))
			{
				return false;
			}
			return myEntity.HasInventory;
		}

		[Obsolete("Use the GetInventoryBase method.")]
		public static IMyInventory GetInventory(this IMyTerminalBlock block, int index)
		{
			MyEntity myEntity = block as MyEntity;
			if (myEntity == null)
			{
				return null;
			}
			if (!myEntity.HasInventory)
			{
				return null;
			}
			return myEntity.GetInventoryBase(index) as IMyInventory;
		}

		[Obsolete("Use the InventoryCount property.")]
		public static int GetInventoryCount(this IMyTerminalBlock block)
		{
			MyEntity myEntity = block as MyEntity;
			if (myEntity == null)
			{
				return 0;
			}
			return myEntity.InventoryCount;
		}

		[Obsolete("Use the blocks themselves, this method is no longer reliable")]
		public static bool GetUseConveyorSystem(this IMyTerminalBlock block)
		{
			if (block is IMyInventoryOwner)
			{
				return ((IMyInventoryOwner)block).UseConveyorSystem;
			}
			return false;
		}

		[Obsolete("Use the blocks themselves, this method is no longer reliable")]
		public static void SetUseConveyorSystem(this IMyTerminalBlock block, bool use)
		{
			if (block is IMyInventoryOwner)
			{
				((IMyInventoryOwner)block).UseConveyorSystem = use;
			}
		}
	}
}
