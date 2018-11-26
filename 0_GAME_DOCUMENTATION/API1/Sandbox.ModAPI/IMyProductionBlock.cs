using Sandbox.ModAPI.Ingame;
using System;
using System.Collections.Generic;
using VRage;
using VRage.Game;
using VRage.Game.ModAPI;
using VRage.Game.ModAPI.Ingame;
using VRage.ModAPI;

namespace Sandbox.ModAPI
{
	public interface IMyProductionBlock : IMyFunctionalBlock, IMyTerminalBlock, VRage.Game.ModAPI.IMyCubeBlock, VRage.ModAPI.IMyEntity, Sandbox.ModAPI.Ingame.IMyProductionBlock, Sandbox.ModAPI.Ingame.IMyFunctionalBlock, Sandbox.ModAPI.Ingame.IMyTerminalBlock, VRage.Game.ModAPI.Ingame.IMyCubeBlock, VRage.Game.ModAPI.Ingame.IMyEntity
	{
		new VRage.Game.ModAPI.IMyInventory InputInventory
		{
			get;
		}

		new VRage.Game.ModAPI.IMyInventory OutputInventory
		{
			get;
		}

		event Action StartedProducing;

		event Action StoppedProducing;

		/// <summary>
		/// Can this production block produce this blueprint?
		/// </summary>
		/// <param name="blueprint">A MyBlueprintDefinition that defines the blueprint</param>
		/// <returns></returns>
		bool CanUseBlueprint(MyDefinitionBase blueprint);

		/// <summary>
		/// Adds a blueprint to the production queue
		/// </summary>
		/// <param name="blueprint">A MyBlueprintDefinition that defines the blueprint</param>
		/// <param name="amount">Amount of items</param>
		void AddQueueItem(MyDefinitionBase blueprint, MyFixedPoint amount);

		/// <summary>
		/// Inserts a blueprint into the production queue
		/// </summary>
		/// <param name="idx">Index of the item</param>
		/// <param name="blueprint">A MyBlueprintDefinition that defines the blueprint</param>
		/// <param name="amount">Amount of items</param>
		void InsertQueueItem(int idx, MyDefinitionBase blueprint, MyFixedPoint amount);

		/// <summary>
		/// Gets the current production queue
		/// </summary>
		/// <returns>List of MyProductionQueueItems</returns>
		List<MyProductionQueueItem> GetQueue();
	}
}
