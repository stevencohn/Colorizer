//************************************************************************************************
// Copyright © 2020 Steven M Cohn.  All rights reserved.
//************************************************************************************************                

namespace River.OneMore.Colorizer
{
	using System;


	internal class LanguageParser
	{
		private readonly CompiledLanguage interpreter;


		public LanguageParser(CompiledLanguage interpreter)
		{
			this.interpreter = interpreter;
		}


		public void Parse(string source, Action<string, string> report)
		{
			var match = interpreter.Regex.Match(source);

			if (!match.Success)
			{
				report(source, null);
				return;
			}

			var index = 0;
			var scope = 0;

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
					report(run, interpreter.Scopes[scope]);
				}

				index = match.Index + match.Length;
				match = match.NextMatch();
				scope++;
			}
		}
	}
}
