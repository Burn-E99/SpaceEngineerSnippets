using Sandbox.ModAPI.Interfaces.Terminal;
using System.Collections.Generic;

namespace Sandbox.ModAPI
{
	/// <summary>
	/// Allows you to modify the actions associated with a block before it's displayed to user. 
	/// </summary>
	/// <param name="block">The block actions are associated with</param>
	/// <param name="actions">The list of actions for this block</param>
	public delegate void CustomActionGetDelegate(IMyTerminalBlock block, List<IMyTerminalAction> actions);
}
