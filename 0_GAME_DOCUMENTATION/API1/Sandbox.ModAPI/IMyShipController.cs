using Sandbox.ModAPI.Ingame;
using VRage.Game.ModAPI;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Interfaces;
using VRage.ModAPI;
using VRageMath;

namespace Sandbox.ModAPI
{
	public interface IMyShipController : IMyTerminalBlock, VRage.Game.ModAPI.IMyCubeBlock, VRage.ModAPI.IMyEntity, Sandbox.ModAPI.Ingame.IMyShipController, Sandbox.ModAPI.Ingame.IMyTerminalBlock, VRage.Game.ModAPI.Ingame.IMyCubeBlock, VRage.Game.ModAPI.Ingame.IMyEntity, IMyControllableEntity
	{
		/// <summary>
		/// Gets if this ship controller contains a first-person camera view.
		/// </summary>
		bool HasFirstPersonCamera
		{
			get;
		}

		/// <summary>
		/// Get the last character that was piloting the controller.
		/// </summary>
		IMyCharacter LastPilot
		{
			get;
		}

		/// <summary>
		/// Get the character that is currently piloting the controller.
		/// </summary>
		IMyCharacter Pilot
		{
			get;
		}

		/// <summary>
		/// Gets if the ship is shooting selected weapons.
		/// </summary>
		bool IsShooting
		{
			get;
		}

		/// <summary>
		/// Gets the current movement direction indicator
		/// </summary>
		/// <remarks>Set by MoveAndRotate, regardless if a movement happened.</remarks>
		new Vector3 MoveIndicator
		{
			get;
		}

		/// <summary>
		/// Gets the current rotation direction indicator
		/// </summary>
		/// <remarks>Set by MoveAndRotate, regardless if a movement happened.</remarks>
		new Vector2 RotationIndicator
		{
			get;
		}

		/// <summary>
		/// Gets the current roll direction indicator
		/// </summary>
		/// <remarks>Set by MoveAndRotate, regardless if a movement happened.</remarks>
		new float RollIndicator
		{
			get;
		}
	}
}
