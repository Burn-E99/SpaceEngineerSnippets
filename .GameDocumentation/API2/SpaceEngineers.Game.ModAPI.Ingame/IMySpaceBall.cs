using Sandbox.ModAPI.Ingame;
using System;
using VRage.Game.ModAPI.Ingame;

namespace SpaceEngineers.Game.ModAPI.Ingame
{
	/// <summary>
	/// Spaceball interface
	/// </summary>
	public interface IMySpaceBall : IMyVirtualMass, IMyFunctionalBlock, IMyTerminalBlock, IMyCubeBlock, IMyEntity
	{
		/// <summary>
		/// Ball friction
		/// </summary>
		float Friction
		{
			get;
			set;
		}

		/// <summary>
		/// Ball restitution
		/// </summary>
		float Restitution
		{
			get;
			set;
		}

		/// <summary>
		/// Is broadcasting
		/// </summary>
		[Obsolete("Use IMySpaceBall.Broadcasting")]
		bool IsBroadcasting
		{
			get;
		}

		/// <summary>
		/// Gets or sets broadcasting
		/// </summary>
		bool Broadcasting
		{
			get;
			set;
		}

		/// <summary>
		/// Virtual mass of ball, in kg
		/// </summary>
		new float VirtualMass
		{
			get;
			set;
		}
	}
}
