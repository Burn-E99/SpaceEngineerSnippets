using VRage.Game.ModAPI.Ingame;

namespace Sandbox.ModAPI.Ingame
{
	public interface IMyAttachableTopBlock : IMyCubeBlock, IMyEntity
	{
		/// <summary>
		/// Gets whether the top part is attached to a base block
		/// </summary>
		bool IsAttached
		{
			get;
		}

		/// <summary>
		/// Gets the attached base block
		/// </summary>
		IMyMechanicalConnectionBlock Base
		{
			get;
		}
	}
}
