using Sandbox.ModAPI.Ingame;
using System;
using VRage.Game.ModAPI;
using VRage.Game.ModAPI.Ingame;
using VRage.ModAPI;
using VRageMath;

namespace Sandbox.ModAPI
{
	public interface IMyMotorBase : IMyMechanicalConnectionBlock, IMyFunctionalBlock, IMyTerminalBlock, VRage.Game.ModAPI.IMyCubeBlock, VRage.ModAPI.IMyEntity, Sandbox.ModAPI.Ingame.IMyMotorBase, Sandbox.ModAPI.Ingame.IMyMechanicalConnectionBlock, Sandbox.ModAPI.Ingame.IMyFunctionalBlock, Sandbox.ModAPI.Ingame.IMyTerminalBlock, VRage.Game.ModAPI.Ingame.IMyCubeBlock, VRage.Game.ModAPI.Ingame.IMyEntity
	{
		/// <summary>
		/// Gets the maximum angular velocity this rotor is capable of.
		/// </summary>
		float MaxRotorAngularVelocity
		{
			get;
		}

		/// <summary>
		/// Gets the current angular velocity.
		/// </summary>
		Vector3 RotorAngularVelocity
		{
			get;
		}

		/// <summary>
		/// Gets the grid attached to the rotor part
		/// </summary>
		[Obsolete("Use IMyMechanicalConnectionBlock.TopGrid")]
		VRage.Game.ModAPI.IMyCubeGrid RotorGrid
		{
			get;
		}

		/// <summary>
		/// Gets the attached rotor part entity
		/// </summary>
		[Obsolete("Use IMyMechanicalConnectionBlock.Top")]
		VRage.Game.ModAPI.IMyCubeBlock Rotor
		{
			get;
		}

		/// <summary>
		/// Gets the dummy position, to aid in attachment
		/// </summary>
		/// <remarks>Gets the location where the top rotor piece will attach.</remarks>
		Vector3 DummyPosition
		{
			get;
		}

		event Action<IMyMotorBase> AttachedEntityChanged;

		/// <summary>
		/// Attaches a specified nearby rotor/wheel to the stator/suspension block
		/// </summary>
		/// <param name="rotor">Entity to attach</param>
		/// <param name="updateGroup">true to update grid groups</param>
		/// <remarks>The rotor to attach must already be in position before calling this method.</remarks>
		void Attach(IMyMotorRotor rotor, bool updateGroup = true);
	}
}
