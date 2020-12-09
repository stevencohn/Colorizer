//************************************************************************************************
// Copyright © 2020 Steven M Cohn.  All rights reserved.
//************************************************************************************************                

namespace River.OneMoreAddIn.Colorizer
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using System.Text.RegularExpressions;


	internal static class LanguageCompiler
	{
		private static readonly Regex captureCountRegex =
			new Regex(@"(?x)(?<!(\\|(?!\\)\(\?))\((?!\?)", RegexOptions.Compiled);


		public static void Compile(ILanguage language)
		{
			var builder = new StringBuilder();

			// ignore pattern whitespace
			builder.AppendLine("(?x)");

			// 0th capture is always the entire string
			var scopes = new List<string>
			{
				"All"
			};

			for (int i = 0; i < language.Rules.Count; i++)
			{
				var rule = language.Rules[i];
				ValidateRule(language, rule, i);

				if (i > 0)
				{
					builder.AppendLine();
					builder.AppendLine("|");
					builder.AppendLine();
				}

				// ?-xis = enables pattern whitespace, case sensitivity, multi line
				// ?m = enables multi line
				builder.Append("(?-xis)(?m)(?:");
				builder.Append(rule.Pattern);
				// ?x ignores pattern whitespace again
				builder.AppendLine(")(?x)");

				scopes.AddRange(rule.Captures);
			}

			var compiled = (Language)language;
			compiled.Rules.Clear();
			compiled.Regex = new Regex(builder.ToString());
			compiled.Scopes = scopes;
		}


		private static void ValidateRule(ILanguage language, ILanguageRule rule, int ruleNum)
		{
			if (string.IsNullOrWhiteSpace(rule.Pattern))
			{
				throw new LanguageException(
					string.Format("{0} rule {1} has an empty pattern", language.Name, ruleNum),
					language.Name, ruleNum);
			}

			if (rule.Captures == null || rule.Captures.Count == 0)
			{
				throw new LanguageException(
					string.Format("{0} rule {1} does not have defined captures", language.Name, ruleNum),
					language.Name, ruleNum);
			}

			var count = captureCountRegex.Matches(rule.Pattern).Count;
			if (count != rule.Captures.Count)
			{
				throw new LanguageException(
					string.Format("{0} rule {1} has misalignment captures", language.Name, ruleNum),
					language.Name, ruleNum);
			}
		}
	}
}
