using System;
using System.Collections.Generic;

namespace Sandbox.ModAPI.Ingame
{
	public interface IMyBlockGroup
	{
		string Name
		{
			get;
		}

		void GetBlocks(List<IMyTerminalBlock> blocks, Func<IMyTerminalBlock, bool> collect = null);

		void GetBlocksOfType<T>(List<IMyTerminalBlock> blocks, Func<IMyTerminalBlock, bool> collect = null) where T : class;

		void GetBlocksOfType<T>(List<T> blocks, Func<T, bool> collect = null) where T : class;
	}
}
