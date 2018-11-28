using VRage.Game.ModAPI.Ingame;

namespace Sandbox.ModAPI.Ingame
{
	public interface IMyBatteryBlock : IMyFunctionalBlock, IMyTerminalBlock, IMyCubeBlock, IMyEntity
	{
		bool HasCapacityRemaining
		{
			get;
		}

		float CurrentStoredPower
		{
			get;
		}

		float MaxStoredPower
		{
			get;
		}

		float CurrentInput
		{
			get;
		}

		float MaxInput
		{
			get;
		}

		float CurrentOutput
		{
			get;
		}

		float MaxOutput
		{
			get;
		}

		bool IsCharging
		{
			get;
		}

		bool OnlyRecharge
		{
			get;
			set;
		}

		bool OnlyDischarge
		{
			get;
			set;
		}

		bool SemiautoEnabled
		{
			get;
			set;
		}
	}
}
