//************************************************************************************************
// Copyright © 2020 Steven M Cohn.  All rights reserved.
//************************************************************************************************                

namespace River.OneMoreAddIn.Colorizer
{
	using System;
	using System.Linq;
	using System.Text.RegularExpressions;

	internal class Parser
	{
		private readonly ICompiledLanguage language;


		public Parser(ICompiledLanguage language)
		{
			this.language = language;
		}


		/// <summary>
		/// Parse the given source code, invoking the specified reporter for each matched rule
		/// </summary>
		/// <param name="source">The source code to parse</param>
		/// <param name="report">
		/// An action to invoke with the piece of source code and its scope name
		/// </param>
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
				if (match.Index > index + 1)
				{
					// default text prior to match
					report(source.Substring(index, match.Index - index), null);
				}

				if (match.Length > 0)
				{
					// Groups will contain a list of all possible captures in the regex, for both
					// successful and unsuccessful captures. The 0th entry is the capture but
					// doesn't indicate the group name. The next Successful entry is this capture
					// and indicates the group name which should be an index offset of the capture
					// in the entire regex; we can use that to index the appropriate scope.

					var group = match.Groups.Cast<Group>().Skip(1).FirstOrDefault(g => g.Success);

					if ((group != null) && int.TryParse(group.Name, out var scope))
					{
						report(match.Value, language.Scopes[scope]);
					}
					else
					{
						// shouldn't happen but report as default text anyway
						report(match.Value, null);
					}

					index = match.Index + match.Length;
				}
				else
				{
					// captured end-of-line?
					var group = match.Groups.Cast<Group>().Skip(1).FirstOrDefault(g => g.Success);

					if ((group != null) && int.TryParse(group.Name, out var scope))
					{
						report(string.Empty, language.Scopes[scope]);
					}
				}

				match = match.NextMatch();
			}

			if (index < source.Length)
			{
				// remaining source after all captures
				report(source.Substring(index), null);
			}
		}
	}
}
