//************************************************************************************************
// Copyright © 2020 Steven M Cohn.  All rights reserved.
//************************************************************************************************                

namespace River.OneMoreAddIn.Colorizer
{
	using System.IO;
	using System.Reflection;
	using System.Text;
	using System.Xml.Linq;


	internal class Colorizer
	{
		private readonly ILanguage language;
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

			language = Provider.LoadLanguage(path);
		}


		public XElement Colorize(string source)
		{
			var parser = new Parser(Compiler.Compile(language));

			var theme = Provider.LoadTheme(Path.Combine(rootPath, $"styles-light.json"));

			var container = new XElement("OEChildren");
			var builder = new StringBuilder();

			parser.Parse(source, (code, scope) =>
			{
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
						builder.Append(code);
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
	}
}
