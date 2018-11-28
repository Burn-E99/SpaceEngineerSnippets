using VRage.Game.ModAPI.Ingame;

namespace Sandbox.ModAPI.Ingame
{
	/// <summary>
	/// Ship welder interface
	/// </summary>
	public interface IMyShipWelder : IMyShipToolBase, IMyFunctionalBlock, IMyTerminalBlock, IMyCubeBlock, IMyEntity
	{
		/// <summary>
		/// True if welder is set to helper mode
		/// </summary>
		bool HelpOthers
		{
			get;
			set;
		}
	}
}
