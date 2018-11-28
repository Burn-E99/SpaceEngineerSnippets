using VRage.Game.ModAPI.Ingame;

namespace Sandbox.ModAPI.Ingame
{
	/// <summary>
	/// Gas generator interface
	/// </summary>
	public interface IMyGasGenerator : IMyFunctionalBlock, IMyTerminalBlock, IMyCubeBlock, IMyEntity
	{
		/// <summary>
		/// Autorefill enabled
		/// </summary>
		bool AutoRefill
		{
			get;
			set;
		}

		/// <summary>
		/// Conveyor system enabled
		/// </summary>
		bool UseConveyorSystem
		{
			get;
			set;
		}
	}
}
