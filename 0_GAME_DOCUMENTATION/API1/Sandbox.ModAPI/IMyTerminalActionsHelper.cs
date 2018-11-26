using Sandbox.ModAPI.Interfaces;
using System;
using System.Collections.Generic;
using VRage.Game.ModAPI;

namespace Sandbox.ModAPI
{
	public interface IMyTerminalActionsHelper
	{
		void GetActions(Type blockType, List<ITerminalAction> resultList, Func<ITerminalAction, bool> collect = null);

		void SearchActionsOfName(string name, Type blockType, List<ITerminalAction> resultList, Func<ITerminalAction, bool> collect = null);

		ITerminalAction GetActionWithName(string nameType, Type blockType);

		ITerminalProperty GetProperty(string id, Type blockType);

		void GetProperties(Type blockType, List<ITerminalProperty> resultList, Func<ITerminalProperty, bool> collect = null);

		IMyGridTerminalSystem GetTerminalSystemForGrid(IMyCubeGrid grid);
	}
}
