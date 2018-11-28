using System;
using System.Reflection;

namespace Sandbox.ModAPI.Ingame
{
	/// <summary>
	///     All programmable block scripts derive from this class, meaning that all properties in this
	///     class are directly available for use in your scripts.
	///     If you use Visual Studio or other external editors to write your scripts, you can derive
	///     directly from this class and have a compatible script template.
	/// </summary>
	/// <example>
	///     <code>
	/// public void Main()
	/// {
	///     // Print out the time elapsed since the currently running programmable block was run
	///     // the last time.
	///     Echo(Me.CustomName + " was last run " + Runtime.TimeSinceLastRun.TotalSeconds + " seconds ago.");
	/// }
	/// </code>
	/// </example>
	public abstract class MyGridProgram : IMyGridProgram
	{
		private string m_storage;

		private readonly Action<string, UpdateType> m_main;

		private readonly Action m_save;

		/// <summary>
		///     Provides access to the grid terminal system as viewed from this programmable block.
		/// </summary>
		public virtual IMyGridTerminalSystem GridTerminalSystem
		{
			get;
			protected set;
		}

		/// <summary>
		///     Gets a reference to the currently running programmable block.
		/// </summary>
		public virtual IMyProgrammableBlock Me
		{
			get;
			protected set;
		}

		/// <summary>
		///     Gets the amount of in-game time elapsed from the previous run.
		/// </summary>
		[Obsolete("Use Runtime.TimeSinceLastRun instead")]
		public virtual TimeSpan ElapsedTime
		{
			get;
			protected set;
		}

		/// <summary>
		/// Gets runtime information for the running grid program.
		/// </summary>
		public virtual IMyGridProgramRuntimeInfo Runtime
		{
			get;
			protected set;
		}

		/// <summary>
		///     Allows you to store data between game sessions.
		/// </summary>
		public virtual string Storage
		{
			get
			{
				return this.m_storage ?? "";
			}
			protected set
			{
				this.m_storage = (value ?? "");
			}
		}

		/// <summary>
		///     Prints out text onto the currently running programmable block's detail info area.
		/// </summary>
		public Action<string> Echo
		{
			get;
			protected set;
		}

		IMyGridTerminalSystem IMyGridProgram.GridTerminalSystem
		{
			get
			{
				return this.GridTerminalSystem;
			}
			set
			{
				this.GridTerminalSystem = value;
			}
		}

		IMyProgrammableBlock IMyGridProgram.Me
		{
			get
			{
				return this.Me;
			}
			set
			{
				this.Me = value;
			}
		}

		TimeSpan IMyGridProgram.ElapsedTime
		{
			get
			{
				return this.ElapsedTime;
			}
			set
			{
				this.ElapsedTime = value;
			}
		}

		string IMyGridProgram.Storage
		{
			get
			{
				return this.Storage;
			}
			set
			{
				this.Storage = value;
			}
		}

		Action<string> IMyGridProgram.Echo
		{
			get
			{
				return this.Echo;
			}
			set
			{
				this.Echo = value;
			}
		}

		IMyGridProgramRuntimeInfo IMyGridProgram.Runtime
		{
			get
			{
				return this.Runtime;
			}
			set
			{
				this.Runtime = value;
			}
		}

		bool IMyGridProgram.HasMainMethod
		{
			get
			{
				return this.m_main != null;
			}
		}

		bool IMyGridProgram.HasSaveMethod
		{
			get
			{
				return this.m_save != null;
			}
		}

		protected MyGridProgram()
		{
			Type type = base.GetType();
			MethodInfo method = type.GetMethod("Main", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[2]
			{
				typeof(string),
				typeof(UpdateType)
			}, null);
			if (method != (MethodInfo)null)
			{
				this.m_main = method.CreateDelegate<Action<string, UpdateType>>(this);
			}
			else
			{
				method = type.GetMethod("Main", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[1]
				{
					typeof(string)
				}, null);
				if (method != (MethodInfo)null)
				{
					Action<string> main = method.CreateDelegate<Action<string>>(this);
					this.m_main = delegate(string arg, UpdateType source)
					{
						main(arg);
					};
				}
				else
				{
					method = type.GetMethod("Main", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
					if (method != (MethodInfo)null)
					{
						Action mainWithoutArgument = method.CreateDelegate<Action>(this);
						this.m_main = delegate
						{
							mainWithoutArgument();
						};
					}
				}
			}
			MethodInfo method2 = type.GetMethod("Save", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (method2 != (MethodInfo)null)
			{
				this.m_save = method2.CreateDelegate<Action>(this);
			}
		}

		[Obsolete]
		void IMyGridProgram.Main(string argument)
		{
			if (this.m_main == null)
			{
				throw new InvalidOperationException("No Main method available");
			}
			this.m_main(argument ?? string.Empty, UpdateType.Mod);
		}

		void IMyGridProgram.Main(string argument, UpdateType updateSource)
		{
			if (this.m_main == null)
			{
				throw new InvalidOperationException("No Main method available");
			}
			this.m_main(argument ?? string.Empty, updateSource);
		}

		void IMyGridProgram.Save()
		{
			if (this.m_save != null)
			{
				this.m_save();
			}
		}
	}
}
