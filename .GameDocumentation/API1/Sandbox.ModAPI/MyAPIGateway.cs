using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using VRage.Game.ModAPI;
using VRage.ModAPI;

namespace Sandbox.ModAPI
{
	/// <summary>
	/// This is entry point for entire scripting possibilities in game
	/// </summary>
	public static class MyAPIGateway
	{
		/// <summary>
		/// Event triggered on gui control created.
		/// </summary>
		[Obsolete("Use IMyGui.GuiControlCreated")]
		public static Action<object> GuiControlCreated;

		/// <summary>
		/// IMyPlayerCollection contains all players that are in world 
		/// </summary>
		public static IMyPlayerCollection Players;

		/// <summary>
		/// IMyCubeBuilder represents building hand 
		/// </summary>
		public static IMyCubeBuilder CubeBuilder;

		/// <summary>
		/// IMyTerminalActionsHelper is helper for terminal actions and allows to access terminal 
		/// </summary>
		public static IMyTerminalActionsHelper TerminalActionsHelper;

		/// <summary>
		/// IMyTerminalControls allows access to adding and removing controls from a block's terminal screen
		/// </summary>
		public static IMyTerminalControls TerminalControls;

		public static IMyUtilities Utilities;

		/// <summary>
		/// IMyMultiplayer  contains multiplayer related things
		/// </summary>
		public static IMyMultiplayer Multiplayer;

		/// <summary>
		/// IMyParallelTask allows to run tasks on background threads 
		/// </summary>
		public static IMyParallelTask Parallel;

		/// <summary>
		/// IMyPhysics contains physics related things (CastRay, etc.)
		/// </summary>
		public static IMyPhysics Physics;

		/// <summary>
		/// IMyGui exposes some useful values from the GUI systems
		/// </summary>
		public static IMyGui Gui;

		public static IMyPrefabManager PrefabManager;

		/// <summary>
		/// Provides mod access to control compilation of ingame scripts
		/// </summary>
		public static IMyIngameScripting IngameScripting;

		/// <summary>
		/// IMyInput allows accessing direct input device states
		/// </summary>
		public static IMyInput Input;

		private static IMyEntities m_entitiesStorage;

		private static IMySession m_sessionStorage;

		/// <summary>
		/// Provides access to the Grid Group system
		/// </summary>
		public static IMyGridGroups GridGroups;

		/// <summary>
		/// IMySession represents session object e.g. current world and its settings
		/// </summary>
		public static IMySession Session
		{
			get
			{
				return MyAPIGateway.m_sessionStorage;
			}
			set
			{
				MyAPIGateway.m_sessionStorage = value;
			}
		}

		/// <summary>
		/// IMyEntities represents all objects that currently in world 
		/// </summary>
		public static IMyEntities Entities
		{
			get
			{
				return MyAPIGateway.m_entitiesStorage;
			}
			set
			{
				MyAPIGateway.m_entitiesStorage = value;
				if (MyAPIGateway.Entities != null)
				{
					IMyEntities entities = MyAPIGateway.Entities;
					MyAPIGatewayShortcuts.RegisterEntityUpdate = entities.RegisterForUpdate;
					IMyEntities entities2 = MyAPIGateway.Entities;
					MyAPIGatewayShortcuts.UnregisterEntityUpdate = entities2.UnregisterForUpdate;
				}
				else
				{
					MyAPIGatewayShortcuts.RegisterEntityUpdate = null;
					MyAPIGatewayShortcuts.UnregisterEntityUpdate = null;
				}
			}
		}

		[Obsolete]
		[Conditional("DEBUG")]
		public static void GetMessageBoxPointer(ref IntPtr pointer)
		{
			IntPtr hModule = MyAPIGateway.LoadLibrary("user32.dll");
			pointer = MyAPIGateway.GetProcAddress(hModule, "MessageBoxW");
		}

		[DllImport("kernel32.dll")]
		private static extern IntPtr LoadLibrary(string dllname);

		[DllImport("kernel32.dll")]
		private static extern IntPtr GetProcAddress(IntPtr hModule, string procname);

		[Obsolete]
		public static void Clean()
		{
			MyAPIGateway.Session = null;
			MyAPIGateway.Entities = null;
			MyAPIGateway.Players = null;
			MyAPIGateway.CubeBuilder = null;
			if (MyAPIGateway.IngameScripting != null)
			{
				MyAPIGateway.IngameScripting.Clean();
			}
			MyAPIGateway.IngameScripting = null;
			MyAPIGateway.TerminalActionsHelper = null;
			MyAPIGateway.Utilities = null;
			MyAPIGateway.Parallel = null;
			MyAPIGateway.Physics = null;
			MyAPIGateway.Multiplayer = null;
			MyAPIGateway.PrefabManager = null;
			MyAPIGateway.Input = null;
			MyAPIGateway.TerminalControls = null;
			MyAPIGateway.GridGroups = null;
		}

		[Obsolete]
		public static StringBuilder DoorBase(string name)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in name)
			{
				if (c == ' ')
				{
					stringBuilder.Append(c);
				}
				byte b = (byte)c;
				for (int j = 0; j < 8; j++)
				{
					stringBuilder.Append(((b & 0x80) != 0) ? "Door" : "Base");
					b = (byte)(b << 1);
				}
			}
			return stringBuilder;
		}
	}
}
