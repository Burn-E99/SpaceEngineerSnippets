using System;
using System.Collections.Generic;
using System.Text;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame;
using VRageMath;

namespace Sandbox.ModAPI.Ingame
{
	public interface IMyTextPanel : IMyFunctionalBlock, IMyTerminalBlock, IMyCubeBlock, IMyEntity
	{
		/// <summary>
		/// The image that is currently shown on the screen.
		///
		/// Returns NULL if there are no images selected OR the screen is in text mode.
		/// </summary>
		string CurrentlyShownImage
		{
			get;
		}

		/// <summary>
		/// Indicates what should be shown on the screen, none being an image.
		/// </summary>
		ShowTextOnScreenFlag ShowOnScreen
		{
			get;
		}

		/// <summary>
		/// Returns true if the ShowOnScreen flag is set to either PUBLIC or PRIVATE
		/// </summary>
		bool ShowText
		{
			get;
		}

		/// <summary>
		/// Gets or sets font size
		/// </summary>
		float FontSize
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets font color
		/// </summary>
		Color FontColor
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets background color
		/// </summary>
		Color BackgroundColor
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the change interval for selected textures
		/// </summary>
		float ChangeInterval
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the font
		/// </summary>
		string Font
		{
			get;
			set;
		}

		bool WritePublicText(string value, bool append = false);

		string GetPublicText();

		bool WritePublicText(StringBuilder value, bool append = false);

		void ReadPublicText(StringBuilder buffer, bool append = false);

		bool WritePublicTitle(string value, bool append = false);

		string GetPublicTitle();

		[Obsolete("LCD private text is deprecated")]
		bool WritePrivateText(string value, bool append = false);

		[Obsolete("LCD private text is deprecated")]
		string GetPrivateText();

		[Obsolete("LCD private text is deprecated")]
		bool WritePrivateTitle(string value, bool append = false);

		[Obsolete("LCD private text is deprecated")]
		string GetPrivateTitle();

		void AddImageToSelection(string id, bool checkExistence = false);

		void AddImagesToSelection(List<string> ids, bool checkExistence = false);

		void RemoveImageFromSelection(string id, bool removeDuplicates = false);

		void RemoveImagesFromSelection(List<string> ids, bool removeDuplicates = false);

		void ClearImagesFromSelection();

		/// <summary>
		/// Outputs the selected image ids to the specified list.
		///
		/// NOTE: List is not cleared internally.
		/// </summary>
		/// <param name="output"></param>
		void GetSelectedImages(List<string> output);

		void ShowPublicTextOnScreen();

		[Obsolete("LCD private text is deprecated")]
		void ShowPrivateTextOnScreen();

		void ShowTextureOnScreen();

		void SetShowOnScreen(ShowTextOnScreenFlag set);

		/// <summary>
		/// Gets a list of available fonts
		/// </summary>
		/// <param name="fonts"></param>
		void GetFonts(List<string> fonts);
	}
}
