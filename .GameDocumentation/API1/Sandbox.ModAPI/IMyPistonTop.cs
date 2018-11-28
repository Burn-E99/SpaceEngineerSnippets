using Sandbox.ModAPI.Ingame;
using VRage.Game.ModAPI;
using VRage.Game.ModAPI.Ingame;
using VRage.ModAPI;

namespace Sandbox.ModAPI
{
	public interface IMyPistonTop : IMyAttachableTopBlock, VRage.Game.ModAPI.IMyCubeBlock, VRage.ModAPI.IMyEntity, Sandbox.ModAPI.Ingame.IMyPistonTop, Sandbox.ModAPI.Ingame.IMyAttachableTopBlock, VRage.Game.ModAPI.Ingame.IMyCubeBlock, VRage.Game.ModAPI.Ingame.IMyEntity
	{
		/// <summary>
		/// Gets the attached piston block
		/// </summary>
		IMyPistonBase Piston
		{
			get;
		}

		/// <summary>
		/// Gets the attached stator/suspension block
		/// </summary>
		new IMyPistonBase Base
		{
			get;
		}
	}
}
