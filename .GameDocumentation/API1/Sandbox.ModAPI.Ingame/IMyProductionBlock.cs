using System.Collections.Generic;
using VRage;
using VRage.Game;
using VRage.Game.ModAPI.Ingame;

namespace Sandbox.ModAPI.Ingame
{
	public interface IMyProductionBlock : IMyFunctionalBlock, IMyTerminalBlock, IMyCubeBlock, IMyEntity
	{
		/// <summary>
		/// Gets the input inventory.
		/// </summary>
		IMyInventory InputInventory
		{
			get;
		}

		/// <summary>
		/// Gets the output inventory.
		/// </summary>
		IMyInventory OutputInventory
		{
			get;
		}

		/// <summary>
		/// The device is currently producing.
		/// </summary>
		bool IsProducing
		{
			get;
		}

		/// <summary>
		/// The production queue is empty.
		/// </summary>
		bool IsQueueEmpty
		{
			get;
		}

		/// <summary>
		/// Gets the queue item ID of the next item to be produced.
		/// </summary>
		uint NextItemId
		{
			get;
		}

		/// <summary>
		/// Gets or sets whether this device should use the conveyor system to retrieve and store items.
		/// </summary>
		bool UseConveyorSystem
		{
			get;
			set;
		}

		/// <summary>
		/// Moves an item in the queue to a target position in the queue.
		/// </summary>
		/// <param name="queueItemId"></param>
		/// <param name="targetIdx"></param>
		void MoveQueueItemRequest(uint queueItemId, int targetIdx);

		/// <summary>
		/// Can this production block produce this blueprint?
		/// </summary>
		/// <param name="blueprint">A MyDefinitionId that names the blueprint</param>
		/// <returns></returns>
		bool CanUseBlueprint(MyDefinitionId blueprint);

		/// <summary>
		/// Adds a blueprint to the production queue
		/// </summary>
		/// <param name="blueprint">A MyDefinitionId that names the blueprint</param>
		/// <param name="amount">Amount of items</param>
		void AddQueueItem(MyDefinitionId blueprint, MyFixedPoint amount);

		/// <summary>
		/// Adds a blueprint to the production queue
		/// </summary>
		/// <param name="blueprint">A MyDefinitionId that names the blueprint</param>
		/// <param name="amount">Amount of items</param>
		void AddQueueItem(MyDefinitionId blueprint, decimal amount);

		/// <summary>
		/// Adds a blueprint to the production queue
		/// </summary>
		/// <param name="blueprint">A MyDefinitionId that names the blueprint</param>
		/// <param name="amount">Amount of items</param>
		void AddQueueItem(MyDefinitionId blueprint, double amount);

		/// <summary>
		/// Inserts a blueprint into the production queue
		/// </summary>
		/// <param name="idx">Index of the item</param>
		/// <param name="blueprint">A MyDefinitionId that names the blueprint</param>
		/// <param name="amount">Amount of items</param>
		void InsertQueueItem(int idx, MyDefinitionId blueprint, MyFixedPoint amount);

		/// <summary>
		/// Inserts a blueprint into the production queue
		/// </summary>
		/// <param name="idx">Index of the item</param>
		/// <param name="blueprint">A MyDefinitionId that names the blueprint</param>
		/// <param name="amount">Amount of items</param>
		void InsertQueueItem(int idx, MyDefinitionId blueprint, decimal amount);

		/// <summary>
		/// Inserts a blueprint into the production queue
		/// </summary>
		/// <param name="idx">Index of the item</param>
		/// <param name="blueprint">A MyDefinitionId that names the blueprint</param>
		/// <param name="amount">Amount of items</param>
		void InsertQueueItem(int idx, MyDefinitionId blueprint, double amount);

		/// <summary>
		/// Removes an item from the queue
		/// </summary>
		/// <param name="idx">Index of the item</param>
		/// <param name="amount">Amount to remove</param>
		void RemoveQueueItem(int idx, MyFixedPoint amount);

		/// <summary>
		/// Removes an item from the queue
		/// </summary>
		/// <param name="idx">Index of the item</param>
		/// <param name="amount">Amount to remove</param>
		void RemoveQueueItem(int idx, decimal amount);

		/// <summary>
		/// Removes an item from the queue
		/// </summary>
		/// <param name="idx">Index of the item</param>
		/// <param name="amount">Amount to remove</param>
		void RemoveQueueItem(int idx, double amount);

		/// <summary>
		/// Clears the Queue
		/// </summary>
		void ClearQueue();

		/// <summary>
		/// Gets the current production queue
		/// </summary>
		/// <returns>List of MyProductionQueueItems</returns>
		void GetQueue(List<MyProductionItem> items);
	}
}
