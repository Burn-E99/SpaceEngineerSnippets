using VRage.Game.ModAPI.Ingame;

namespace Sandbox.ModAPI.Ingame
{
	/// <summary>
	/// Antenna block interface
	/// </summary>
	public interface IMyRadioAntenna : IMyFunctionalBlock, IMyTerminalBlock, IMyCubeBlock, IMyEntity
	{
		/// <summary>
		/// Broadcasting/Receiving range
		/// </summary>
		float Radius
		{
			get;
			set;
		}

		/// <summary>
		/// Show shipname on hud
		/// </summary>
		bool ShowShipName
		{
			get;
			set;
		}

		/// <summary>
		/// Returns true if antena is broadcasting
		/// </summary>
		bool IsBroadcasting
		{
			get;
		}

		/// <summary>
		/// Gets or sets if broadcasting is enabled
		/// </summary>
		bool EnableBroadcasting
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the EntityID of the Programmable Block attached to this antenna.
		/// </summary>
		long AttachedProgrammableBlock
		{
			get;
			set;
		}

		/// <summary>
		/// Ignores broadcasts sent by friendly antenna that do not belong to you
		/// </summary>
		bool IgnoreAlliedBroadcast
		{
			get;
			set;
		}

		/// <summary>
		/// Ignores broadcasts sent by neutral and enemy antenna
		/// </summary>
		bool IgnoreOtherBroadcast
		{
			get;
			set;
		}

		/// <summary>
		/// Broadcasts a message to all PB attached to the antenna system.
		/// Broadcast is delayed until the start of the next tick, and only one transmission can be sent per tick.
		/// Returns false if broadcasting failed for any reason.
		/// Limited to 100,000 characters.
		/// </summary>
		/// <param name="message"></param>
		/// <param name="target"></param>
		bool TransmitMessage(string message, MyTransmitTarget target = MyTransmitTarget.Default);
	}
}
