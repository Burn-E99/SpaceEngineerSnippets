using Sandbox.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame;

namespace SpaceEngineers.Game.ModAPI.Ingame
{
	public interface IMyButtonPanel : IMyTerminalBlock, IMyCubeBlock, IMyEntity
	{
		/// <summary>
		/// Gets or sets if anyone is allowed to activate the buttons.
		/// </summary>
		bool AnyoneCanUse
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the button name.
		/// </summary>
		/// <param name="index">Zero-based button position</param>
		/// <returns></returns>
		string GetButtonName(int index);

		/// <summary>
		/// Sets the custom button name.
		/// </summary>
		/// <param name="index">Zero-base button position</param>
		/// <param name="name">Name of button</param>
		void SetCustomButtonName(int index, string name);

		/// <summary>
		/// Gets if the specified button has a custom name set.
		/// </summary>
		/// <param name="index">Zero-base button position</param>
		/// <returns></returns>
		bool HasCustomButtonName(int index);

		/// <summary>
		/// Clears the custom name of the specified button.
		/// </summary>
		/// <param name="index">Zero-base button position</param>
		/// <remarks>This is safe to call even if there is no custom name assigned.</remarks>
		void ClearCustomButtonName(int index);

		/// <summary>
		/// Gets if the specified button is assigned an action.
		/// </summary>
		/// <param name="index">Zero-base button position</param>
		/// <returns></returns>
		bool IsButtonAssigned(int index);
	}
}
