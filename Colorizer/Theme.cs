//************************************************************************************************
// Copyright © 2020 Steven M Cohn.  All rights reserved.
//************************************************************************************************                

namespace River.OneMoreAddIn.Colorizer
{
	using System.Collections.Generic;
	using System.Drawing;
	using System.Linq;
	using System.Text;


	// = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
	// Style...

	/// <summary>
	/// 
	/// </summary>
	internal interface IStyle
	{
		string Name { get; }
		string Foreground { get; }
		string Background { get; }
		bool Bold { get; }
		bool Italic { get; }

		string Apply(string code);
	}


	/// <summary>
	/// Used only for deserialization; the interface is used thereafter
	/// </summary>
	internal class Style : IStyle
	{
		public string Name { get; set; }
		public string Foreground { get; set; }
		public string Background { get; set; }
		public bool Bold { get; set; }
		public bool Italic { get; set; }

		public string Apply(string code)
		{
			var builder = new StringBuilder();
			if (!string.IsNullOrEmpty(Foreground))
				builder.Append($"color:{Foreground};");

			if (!string.IsNullOrEmpty(Background))
				builder.Append($"background:{Background};");

			if (Bold)
				builder.Append("font-weight:bold;");

			if (Italic)
				builder.Append("font-style:italic;");

			if (builder.Length > 0)
			{
				return $"<span style=\"{builder}\">{code}</span>";
			}

			return code;
		}
	}


	// = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
	// Theme...

	/// <summary>
	/// 
	/// </summary>
	internal interface ITheme
	{
		Dictionary<string, string> Colors { get; set; }

		List<IStyle> Styles { get; set; }

		IStyle GetStyle(string name);
	}


	/// <summary>
	/// Used only for deserialization; the interface is used thereafter
	/// </summary>
	internal class Theme : ITheme
	{
		private const string DefaultPlainText = "#FF000000";

		public Dictionary<string, string> Colors { get; set; }

		public List<IStyle> Styles { get; set; }

		public IStyle GetStyle(string name)
		{
			return Styles.FirstOrDefault(s => s.Name == name);
		}


		public void TranslateColorNames()
		{
			foreach (Style style in Styles)
			{
				style.Background = TranslateColorName(style.Background);
				style.Foreground = TranslateColorName(style.Foreground);
			}
		}

		public string TranslateColorName(string color)
		{
			if (string.IsNullOrEmpty(color))
			{
				return null;
			}

			if (Colors.ContainsKey(color))
			{
				color = Colors[color];

				// normalize color as 6-byte hex HTML color string

				//return ColorTranslator.FromHtml(color).ToRGBHtml();
				var c = ColorTranslator.FromHtml(color);
				return $"#{c.R:X2}{c.G:X2}{c.B:X2}";
			}

			if (color.StartsWith("#"))
			{
				var c = ColorTranslator.FromHtml(color);
				return $"#{c.R:X2}{c.G:X2}{c.B:X2}";
			}

			return color;
		}
	}
}
