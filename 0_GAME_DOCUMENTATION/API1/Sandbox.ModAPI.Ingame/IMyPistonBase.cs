using VRage.Game.ModAPI.Ingame;

namespace Sandbox.ModAPI.Ingame
{
	public interface IMyPistonBase : IMyMechanicalConnectionBlock, IMyFunctionalBlock, IMyTerminalBlock, IMyCubeBlock, IMyEntity
	{
		/// <summary>
		/// Gets or sets the velocity of the piston as it extends or retracts. This value can be between negative and positive <see cref="P:Sandbox.ModAPI.Ingame.IMyPistonBase.MaxVelocity" />.
		/// </summary>
		float Velocity
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the maximum velocity this piston is capable of moving at.
		/// </summary>
		float MaxVelocity
		{
			get;
		}

		/// <summary>
		/// Gets or sets the minimum position the piston can retract to. See <see cref="P:Sandbox.ModAPI.Ingame.IMyPistonBase.LowestPosition" /> and <see cref="P:Sandbox.ModAPI.Ingame.IMyPistonBase.HighestPosition" /> for the limits of this value.
		/// </summary>
		float MinLimit
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the maximum position the piston can extend to. See <see cref="P:Sandbox.ModAPI.Ingame.IMyPistonBase.LowestPosition" /> and <see cref="P:Sandbox.ModAPI.Ingame.IMyPistonBase.HighestPosition" /> for the limits of this value.
		/// </summary>
		float MaxLimit
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the lowest position the piston is capable of moving to.
		/// </summary>
		float LowestPosition
		{
			get;
		}

		/// <summary>
		/// Gets the highest position the piston is capable of moving to.
		/// </summary>
		float HighestPosition
		{
			get;
		}

		/// <summary>
		/// Gets the current position of the piston head relative to the base.
		/// </summary>
		float CurrentPosition
		{
			get;
		}

		/// <summary>
		/// Gets the current status.
		/// </summary>
		PistonStatus Status
		{
			get;
		}

		/// <summary>
		/// Extends the piston.
		/// </summary>
		void Extend();

		/// <summary>
		/// Retracts the piston.
		/// </summary>
		void Retract();

		/// <summary>
		/// Reverses the direction of the piston.
		/// </summary>
		void Reverse();
	}
}
