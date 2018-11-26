using System;
using VRage.Game.ModAPI.Ingame;

namespace Sandbox.ModAPI.Ingame
{
	public interface IMyFunctionalBlock : IMyTerminalBlock, IMyCubeBlock, IMyEntity
	{
		bool Enabled
		{
			get;
			set;
		}

		[Obsolete("Use the setter of Enabled")]
		void RequestEnable(bool enable);
	}
}
