//************************************************************************************************
// Copyright © 2020 Steven M Cohn.  All rights reserved.
//************************************************************************************************                

namespace River.OneMoreAddIn.Colorizer
{
	using System;
	using System.Linq;
	using System.Text.RegularExpressions;

	internal class LanguageParser
	{
		private readonly ICompiledLanguage language;


		public LanguageParser(ICompiledLanguage language)
		{
			this.language = language;
		}


		public void Parse(string source, Action<string, string> report)
		{
			var match = language.Regex.Match(source);

			if (!match.Success)
			{
				report(source, null);
				return;
			}

			var index = 0;

			while (match.Success)
			{
				if (match.Index > index)
				{
					// default text prior to match
					report(source.Substring(index, match.Index - index), null);
				}

				var run = source.Substring(match.Index, match.Length);
				if (!string.IsNullOrEmpty(run))
				{

					var group = match.Groups.Cast<Group>().Skip(1).FirstOrDefault(g => g.Success);
					if (int.TryParse(group.Name, out var scope))
					{
						report(run, language.Scopes[scope]);
					}
					else
					{
						// shouldn't happen but report as default text anyway
						report(run, null);
					}
				}

				index = match.Index + match.Length;
				match = match.NextMatch();
			}
		}
	}
}
