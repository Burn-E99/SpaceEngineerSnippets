using Sandbox.ModAPI.Ingame;
using System;
using VRage.Game.ModAPI.Ingame;
using VRageMath;

namespace SpaceEngineers.Game.ModAPI.Ingame
{
	public interface IMyGravityGenerator : IMyGravityGeneratorBase, IMyFunctionalBlock, IMyTerminalBlock, IMyCubeBlock, IMyEntity
	{
		[Obsolete("Use FieldSize.X")]
		float FieldWidth
		{
			get;
		}

		[Obsolete("Use FieldSize.Y")]
		float FieldHeight
		{
			get;
		}

		[Obsolete("Use FieldSize.Z")]
		float FieldDepth
		{
			get;
		}

		/// <summary>
		/// Gets or sets the gravity field as a Vector3(W,H,D).
		/// </summary>
		/// <remarks>
		/// X is Width
		/// Y is Height
		/// Z is Depth
		/// </remarks>
		Vector3 FieldSize
		{
			get;
			set;
		}
	}
}
