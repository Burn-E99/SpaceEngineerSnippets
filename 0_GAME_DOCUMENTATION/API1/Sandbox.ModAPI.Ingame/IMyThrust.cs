using VRage.Game.ModAPI.Ingame;
using VRageMath;

namespace Sandbox.ModAPI.Ingame
{
	public interface IMyThrust : IMyFunctionalBlock, IMyTerminalBlock, IMyCubeBlock, IMyEntity
	{
		/// <summary>
		/// Gets or sets the override thrust amount, in Newtons (N)
		/// </summary>
		float ThrustOverride
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the thrust override as a percentage between 0 and 1
		/// </summary>
		float ThrustOverridePercentage
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the maximum thrust amount, in Newtons (N)
		/// </summary>
		float MaxThrust
		{
			get;
		}

		/// <summary>
		/// Gets the maximum effective thrust amount, in Newtons (N)
		/// </summary>
		float MaxEffectiveThrust
		{
			get;
		}

		/// <summary>
		/// Gets the current thrust amount, in Newtons (N)
		/// </summary>
		float CurrentThrust
		{
			get;
		}

		/// <summary>
		/// Gets the direction of thrust, relative to ship controller (cockpit).
		/// </summary>
		/// <remarks>
		/// For example, a value of <see cref="F:VRageMath.Vector3I.Forward">VRageMath.Vector3I.Forward</see> means the thruster will move the grid along the grid's forward direction.
		/// A value of Vector3D.Zero means direction is unknown (no cockpit available).
		/// </remarks>
		Vector3I GridThrustDirection
		{
			get;
		}
	}
}
