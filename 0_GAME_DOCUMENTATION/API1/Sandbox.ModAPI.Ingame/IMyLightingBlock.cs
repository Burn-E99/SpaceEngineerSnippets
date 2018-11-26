using System;
using VRage.Game.ModAPI.Ingame;
using VRageMath;

namespace Sandbox.ModAPI.Ingame
{
	public interface IMyLightingBlock : IMyFunctionalBlock, IMyTerminalBlock, IMyCubeBlock, IMyEntity
	{
		/// <summary>
		/// Gets or sets the basic radius of the light.
		/// </summary>
		float Radius
		{
			get;
			set;
		}

		[Obsolete("Use Radius")]
		float ReflectorRadius
		{
			get;
		}

		/// <summary>
		/// Gets or sets the current intensity of the light.
		/// </summary>
		float Intensity
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the current falloff of the light.
		/// </summary>
		float Falloff
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the blinking interval of this light (in seconds).
		/// </summary>
		float BlinkIntervalSeconds
		{
			get;
			set;
		}

		[Obsolete("Use BlinkLength instead.")]
		float BlinkLenght
		{
			get;
		}

		/// <summary>
		/// Gets or sets how much of the blinking interval should be spent with the light on, as a value between 0 and 1.
		/// </summary>
		float BlinkLength
		{
			get;
			set;
		}

		/// <summary>
		/// Adds an offset to the blinking interval, as a value between 0 and 1.
		/// </summary>
		float BlinkOffset
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the color of the light.
		/// </summary>
		Color Color
		{
			get;
			set;
		}
	}
}
