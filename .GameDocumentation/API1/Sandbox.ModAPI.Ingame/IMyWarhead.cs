using VRage.Game.ModAPI.Ingame;

namespace Sandbox.ModAPI.Ingame
{
	public interface IMyWarhead : IMyTerminalBlock, IMyCubeBlock, IMyEntity
	{
		bool IsCountingDown
		{
			get;
		}

		float DetonationTime
		{
			get;
			set;
		}

		bool IsArmed
		{
			get;
			set;
		}

		bool StartCountdown();

		bool StopCountdown();

		void Detonate();
	}
}
