using Sandbox.ModAPI.Ingame;
using System;
using VRage.Game.ModAPI.Ingame;

namespace SpaceEngineers.Game.ModAPI.Ingame
{
	/// <summary>
	/// AirVent block for pressurizing and depresurizing rooms
	/// </summary>
	public interface IMyAirVent : IMyFunctionalBlock, IMyTerminalBlock, IMyCubeBlock, IMyEntity
	{
		/// <summary>
		/// Can fill room with air 
		/// true - room is airtight
		/// false - room is not airtight
		/// </summary>
		bool CanPressurize
		{
			get;
		}

		/// <summary>
		/// Vent mode
		/// false - pressurize (filling room)
		/// true - depressurize (sucking air out)
		/// </summary>
		[Obsolete("IsDepressurizing is deprecated, please use Depressurize instead.")]
		bool IsDepressurizing
		{
			get;
		}

		/// <summary>
		/// Vent mode
		/// false - pressurize (filling room)
		/// true - depressurize (sucking air out)
		/// </summary>
		bool Depressurize
		{
			get;
			set;
		}

		/// <summary>
		/// Gets current air vent status
		/// </summary>
		VentStatus Status
		{
			get;
		}

		/// <summary>
		/// Returns true if pressurization is enabled.
		/// </summary>
		bool PressurizationEnabled
		{
			get;
		}

		/// <summary>
		/// Room can be pressurized
		/// </summary>
		/// <returns>true if containing room is airtight</returns>
		[Obsolete("IsPressurized() is deprecated, please use CanPressurize instead.")]
		bool IsPressurized();

		/// <summary>
		/// Oxygen level in room
		/// </summary>
		/// <returns>Oxygen fill level as decimal (0.5 = 50%)</returns>
		float GetOxygenLevel();
	}
}
