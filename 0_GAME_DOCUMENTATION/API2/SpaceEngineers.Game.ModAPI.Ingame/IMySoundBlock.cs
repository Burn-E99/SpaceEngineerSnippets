using Sandbox.ModAPI.Ingame;
using System.Collections.Generic;
using VRage.Game.ModAPI.Ingame;

namespace SpaceEngineers.Game.ModAPI.Ingame
{
	public interface IMySoundBlock : IMyFunctionalBlock, IMyTerminalBlock, IMyCubeBlock, IMyEntity
	{
		/// <summary>
		/// Gets or sets the volume level of sound
		/// </summary>
		float Volume
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the range the sound is audible.
		/// </summary>
		float Range
		{
			get;
			set;
		}

		/// <summary>
		/// Gets if a sound is currently selected.
		/// </summary>
		bool IsSoundSelected
		{
			get;
		}

		/// <summary>
		/// Gets or sets the loop period of a loopable sound, in seconds.
		/// </summary>
		/// <remarks>This value is ignored if the selected sound is not loopable.</remarks>
		float LoopPeriod
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the selected sound.
		/// </summary>
		/// <remarks>The sound can be selected either by using the unique hash name, or the user visible text.<p />
		/// Fetching the name will always return the unique hash name.
		/// </remarks>
		string SelectedSound
		{
			get;
			set;
		}

		/// <summary>
		/// Plays the currently selected sound.
		/// </summary>
		void Play();

		/// <summary>
		/// Stops the currently playing sound.
		/// </summary>
		void Stop();

		/// <summary>
		/// Gets a list of all sound IDs this block can use.
		/// </summary>
		/// <param name="sounds"></param>
		void GetSounds(List<string> sounds);
	}
}
