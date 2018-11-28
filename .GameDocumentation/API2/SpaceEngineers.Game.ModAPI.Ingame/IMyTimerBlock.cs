using Sandbox.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame;

namespace SpaceEngineers.Game.ModAPI.Ingame
{
	public interface IMyTimerBlock : IMyFunctionalBlock, IMyTerminalBlock, IMyCubeBlock, IMyEntity
	{
		/// <summary>
		/// Gets if the timer block is active and counting down
		/// </summary>
		bool IsCountingDown
		{
			get;
		}

		/// <summary>
		/// Gets or sets the countdown time, in seconds
		/// </summary>
		float TriggerDelay
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets if the countdown is silent
		/// </summary>
		bool Silent
		{
			get;
			set;
		}

		/// <summary>
		/// Trigger immediately, skips countdown
		/// </summary>
		void Trigger();

		/// <summary>
		/// Begin countdown
		/// </summary>
		void StartCountdown();

		/// <summary>
		/// Stops current countdown
		/// </summary>
		void StopCountdown();
	}
}
