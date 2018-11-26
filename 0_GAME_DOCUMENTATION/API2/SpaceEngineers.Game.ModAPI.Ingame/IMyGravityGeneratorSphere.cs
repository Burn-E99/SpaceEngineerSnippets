using Sandbox.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame;

namespace SpaceEngineers.Game.ModAPI.Ingame
{
	public interface IMyGravityGeneratorSphere : IMyGravityGeneratorBase, IMyFunctionalBlock, IMyTerminalBlock, IMyCubeBlock, IMyEntity
	{
		/// <summary>
		/// Radius of the gravity field, in meters
		/// </summary>
		float Radius
		{
			get;
			set;
		}
	}
}
