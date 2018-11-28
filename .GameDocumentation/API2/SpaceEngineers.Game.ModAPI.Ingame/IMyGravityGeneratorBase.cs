using Sandbox.ModAPI.Ingame;
using System;
using VRage.Game.ModAPI.Ingame;

namespace SpaceEngineers.Game.ModAPI.Ingame
{
	public interface IMyGravityGeneratorBase : IMyFunctionalBlock, IMyTerminalBlock, IMyCubeBlock, IMyEntity
	{
		/// <summary>
		/// Gets the gravity acceleration
		/// </summary>
		[Obsolete("Use GravityAcceleration.")]
		float Gravity
		{
			get;
		}

		/// <summary>
		/// Gets or sets the gravity acceleration in m/s^2.
		/// </summary>
		float GravityAcceleration
		{
			get;
			set;
		}
	}
}
