using Sandbox.ModAPI.Ingame;
using VRage.Game.ModAPI;
using VRage.Game.ModAPI.Ingame;
using VRage.ModAPI;

namespace Sandbox.ModAPI
{
	public interface IMyProgrammableBlock : IMyFunctionalBlock, IMyTerminalBlock, VRage.Game.ModAPI.IMyCubeBlock, VRage.ModAPI.IMyEntity, Sandbox.ModAPI.Ingame.IMyProgrammableBlock, Sandbox.ModAPI.Ingame.IMyFunctionalBlock, Sandbox.ModAPI.Ingame.IMyTerminalBlock, VRage.Game.ModAPI.Ingame.IMyCubeBlock, VRage.Game.ModAPI.Ingame.IMyEntity
	{
		/// <summary>
		/// Program contents. Automatically recompiles when set, if possible.
		/// </summary>
		string ProgramData
		{
			get;
			set;
		}

		/// <summary>
		/// Program storage (server only!)
		/// </summary>
		string StorageData
		{
			get;
			set;
		}

		/// <summary>
		/// Returns true if the script has compile errors (server only!)
		/// </summary>
		bool HasCompileErrors
		{
			get;
		}

		/// <summary>
		/// Recompiles script
		/// </summary>
		void Recompile();

		/// <summary>
		/// Runs with default terminal argument
		/// </summary>
		void Run();

		/// <summary>
		/// Runs with specified argument
		/// </summary>
		/// <param name="argument"></param>
		void Run(string argument);

		/// <summary>
		/// Runs with the specified argument and update source
		/// </summary>
		/// <param name="argument"></param>
		/// <param name="updateSource"></param>
		void Run(string argument, UpdateType updateSource);

		/// <summary>
		/// Attempts to run this programmable block using the given argument. An already running
		/// programmable block cannot be run again.
		/// This is equivalent to running <c>block.ApplyAction("Run", argumentsList);</c>
		/// This should be called from an ingame script. Do not use in mods.
		/// </summary>
		/// <param name="argument"></param>
		/// <returns><c>true</c> if the action was applied, <c>false</c> otherwise</returns>
		new bool TryRun(string argument);
	}
}
