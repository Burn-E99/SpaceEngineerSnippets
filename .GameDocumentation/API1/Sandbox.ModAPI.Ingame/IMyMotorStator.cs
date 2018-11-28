using VRage.Game.ModAPI.Ingame;

namespace Sandbox.ModAPI.Ingame
{
	public interface IMyMotorStator : IMyMotorBase, IMyMechanicalConnectionBlock, IMyFunctionalBlock, IMyTerminalBlock, IMyCubeBlock, IMyEntity
	{
		/// <summary>
		/// Gets the current angle of the rotor in radians.
		/// </summary>
		float Angle
		{
			get;
		}

		/// <summary>
		/// Gets or sets the torque applied when moving the rotor top
		/// </summary>
		float Torque
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the torque applied when stopping the rotor top
		/// </summary>
		float BrakingTorque
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the desired velocity of the rotor in radians/second
		/// </summary>
		float TargetVelocityRad
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the desired velocity of the rotor in RPM
		/// </summary>
		float TargetVelocityRPM
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the lower angle limit of the rotor in radians. Set to float.MinValue for no limit.
		/// </summary>
		float LowerLimitRad
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the lower angle limit of the rotor in degrees. Set to float.MinValue for no limit.
		/// </summary>
		float LowerLimitDeg
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the upper angle limit of the rotor in radians. Set to float.MaxValue for no limit.
		/// </summary>
		float UpperLimitRad
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the upper angle limit of the rotor in degrees. Set to float.MaxValue for no limit.
		/// </summary>
		float UpperLimitDeg
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the vertical displacement of the rotor top
		/// </summary>
		float Displacement
		{
			get;
			set;
		}
	}
}
