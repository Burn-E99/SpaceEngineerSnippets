using System;
using VRage.Game.ModAPI.Ingame;

namespace Sandbox.ModAPI.Ingame
{
	public interface IMyAssembler : IMyProductionBlock, IMyFunctionalBlock, IMyTerminalBlock, IMyCubeBlock, IMyEntity
	{
		[Obsolete("Use the Mode property")]
		bool DisassembleEnabled
		{
			get;
		}

		/// <summary>
		/// Gets the progress for the item currently in production.
		/// </summary>
		float CurrentProgress
		{
			get;
		}

		/// <summary>
		/// Gets or sets the current work mode of this assembly, whether it's assembling or disassembling.
		/// </summary>
		MyAssemblerMode Mode
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets whether this assembler should cooperate with other assemblers by adopting parts of
		/// their work queue.
		/// </summary>
		bool CooperativeMode
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets whether this assembler should be perpetually repeating its work queue.
		/// </summary>
		bool Repeating
		{
			get;
			set;
		}
	}
}
