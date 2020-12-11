//************************************************************************************************
// Copyright © 2020 Steven M Cohn.  All rights reserved.
//************************************************************************************************                

namespace River.OneMoreAddIn.Colorizer
{
	using System.IO;
	using System.Reflection;
	using System.Text;
	using System.Xml.Linq;


	/// <summary>
	/// This colorizer is suited specifically to generating OneNote content
	/// </summary>
	internal class Colorizer
	{
		private readonly Parser parser;
		private readonly ITheme theme;
		private readonly string rootPath;


		public Colorizer(string languageName)
		{
			rootPath = Path.Combine(
				Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
				"Languages");

			var path = Path.Combine(rootPath, $"{languageName}.json");

			if (!File.Exists(path))
			{
				throw new FileNotFoundException(path);
			}

			parser = new Parser(Compiler.Compile(Provider.LoadLanguage(path)));

			theme = Provider.LoadTheme(Path.Combine(rootPath, $"styles-light.json"));
		}


		public XElement Colorize(string source)
		{
			var container = new XElement("OEChildren");
			var builder = new StringBuilder();

			parser.Parse(source, (code, scope) =>
			{
				//System.Console.WriteLine($"'{code}' ({scope})");

				if (string.IsNullOrEmpty(code))
				{
					// end-of-line
					container.Add(new XElement("OE",
						new XElement("T",
							new XCData(builder.ToString()))
						));

					builder.Clear();
				}
				else
				{
					if (scope == null)
					{
						// plain text prior to capture
						builder.Append(code.Replace("\t", " "));
					}
					else
					{
						var style = theme.GetStyle(scope);
						builder.Append(style == null ? code : style.Apply(code));
					}
				}
			});

			return container;
		}


		public string ColorizeOne(string source)
		{
			var builder = new StringBuilder();

			parser.Parse(source, (code, scope) =>
			{
				//System.Console.WriteLine($"'{code}' ({scope})");

				code = System.Web.HttpUtility.HtmlEncode(code);

				if (string.IsNullOrEmpty(code) && parser.HasMoreCaptures)
				{
					// end-of-line
					builder.Append("<br/>");
				}
				else
				{
					if (scope == null)
					{
						// plain text prior to capture
						// simple conversion of tabs to spaces (shouldn't be tabs in OneNote)
						builder.Append(code.Replace("\t", " "));
					}
					else
					{
						var style = theme.GetStyle(scope);
						builder.Append(style == null ? code : style.Apply(code));
					}
				}
			});

			return builder.ToString();
		}
	}
}
