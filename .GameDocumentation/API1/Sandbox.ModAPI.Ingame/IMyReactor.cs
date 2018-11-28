using VRage.Game.ModAPI.Ingame;

namespace Sandbox.ModAPI.Ingame
{
	public interface IMyReactor : IMyFunctionalBlock, IMyTerminalBlock, IMyCubeBlock, IMyEntity
	{
		bool UseConveyorSystem
		{
			get;
			set;
		}

		/// <summary>
		/// Current output of reactor in Megawatts
		/// </summary>
		float CurrentOutput
		{
			get;
		}

		/// <summary>
		/// Maximum output of reactor in Megawatts
		/// </summary>
		float MaxOutput
		{
			get;
		}
	}
}
