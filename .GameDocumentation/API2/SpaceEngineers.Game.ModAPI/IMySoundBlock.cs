using Sandbox.ModAPI;
using Sandbox.ModAPI.Ingame;
using SpaceEngineers.Game.ModAPI.Ingame;
using VRage.Game.ModAPI;
using VRage.Game.ModAPI.Ingame;
using VRage.ModAPI;

namespace SpaceEngineers.Game.ModAPI
{
	public interface IMySoundBlock : Sandbox.ModAPI.IMyFunctionalBlock, Sandbox.ModAPI.IMyTerminalBlock, VRage.Game.ModAPI.IMyCubeBlock, VRage.ModAPI.IMyEntity, SpaceEngineers.Game.ModAPI.Ingame.IMySoundBlock, Sandbox.ModAPI.Ingame.IMyFunctionalBlock, Sandbox.ModAPI.Ingame.IMyTerminalBlock, VRage.Game.ModAPI.Ingame.IMyCubeBlock, VRage.Game.ModAPI.Ingame.IMyEntity
	{
		/// <summary>
		/// Gets or sets the volume level of sound
		/// </summary>
		/// <remarks>This is not clamped like the Ingame one.</remarks>
		new float Volume
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the range the sound is audible.
		/// </summary>
		/// <remarks>This is not clamped like the Ingame one.</remarks>
		new float Range
		{
			get;
			set;
		}
	}
}
