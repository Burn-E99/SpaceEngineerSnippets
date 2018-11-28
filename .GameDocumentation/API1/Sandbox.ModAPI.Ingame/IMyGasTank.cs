using VRage.Game.ModAPI.Ingame;

namespace Sandbox.ModAPI.Ingame
{
	public interface IMyGasTank : IMyFunctionalBlock, IMyTerminalBlock, IMyCubeBlock, IMyEntity
	{
		/// <summary>
		/// Gets or sets the stockpiling option for this gas tank. When stockpile is on, the
		/// tank will only allow itself to be filled, it will not release any gas.
		/// </summary>
		bool Stockpile
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets whether storage bottles will be filled automatically when placed within
		/// this tank.
		/// </summary>
		bool AutoRefillBottles
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the gas capacity of this tank.
		/// </summary>
		float Capacity
		{
			get;
		}

		/// <summary>
		/// Gets the current fill level of this tank as a value between 0 (empty) and 1 (full).
		/// </summary>
		double FilledRatio
		{
			get;
		}

		/// <summary>
		/// Refills any stored storage bottles.
		/// </summary>
		void RefillBottles();
	}
}
