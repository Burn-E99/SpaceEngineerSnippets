using Sandbox.ModAPI.Ingame;
using System;
using System.Collections.Generic;

namespace Sandbox.ModAPI
{
	public interface IMyGridTerminalSystem : Sandbox.ModAPI.Ingame.IMyGridTerminalSystem
	{
		void GetBlocks(List<IMyTerminalBlock> blocks);

		void GetBlockGroups(List<IMyBlockGroup> blockGroups);

		void GetBlocksOfType<T>(List<IMyTerminalBlock> blocks, Func<IMyTerminalBlock, bool> collect = null);

		void SearchBlocksOfName(string name, List<IMyTerminalBlock> blocks, Func<IMyTerminalBlock, bool> collect = null);

		new IMyTerminalBlock GetBlockWithName(string name);

		new IMyBlockGroup GetBlockGroupWithName(string name);
	}
}
