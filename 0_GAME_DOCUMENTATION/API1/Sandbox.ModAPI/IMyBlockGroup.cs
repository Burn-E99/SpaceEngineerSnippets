using Sandbox.ModAPI.Ingame;
using System;
using System.Collections.Generic;

namespace Sandbox.ModAPI
{
	public interface IMyBlockGroup : Sandbox.ModAPI.Ingame.IMyBlockGroup
	{
		void GetBlocks(List<IMyTerminalBlock> blocks, Func<IMyTerminalBlock, bool> collect = null);

		void GetBlocksOfType<T>(List<IMyTerminalBlock> blocks, Func<IMyTerminalBlock, bool> collect = null) where T : class;
	}
}
