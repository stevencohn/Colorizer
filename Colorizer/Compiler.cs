﻿//************************************************************************************************
// Copyright © 2020 Steven M Cohn.  All rights reserved.
//************************************************************************************************                

namespace River.OneMoreAddIn.Colorizer
{
	using System.Collections.Generic;
	using System.Text;
	using System.Text.RegularExpressions;


	internal static class Compiler
	{
		private static readonly Regex capturePattern =
			new Regex(@"(?x)(?<!(\\|(?!\\)\(\?))\((?!\?)", RegexOptions.Compiled);

		private static readonly Regex namedPattern =
			new Regex(@"(?<!(\\|(?!\\)\(\?))\((\?<\w+>)", RegexOptions.Compiled);


		public static ICompiledLanguage Compile(ILanguage language)
		{
			var builder = new StringBuilder();

			// ignore pattern whitespace (?x)
			// include end-of-line capture ($)
			builder.AppendLine("(?x)(?-xis)(?m)($)(?x)");

			// 0th capture is always the entire string
			// 1st capture is always the end-of-line
			var scopes = new List<string>
			{
				"*", "$"
			};

			for (int i = 0; i < language.Rules.Count; i++)
			{
				var rule = language.Rules[i];
				ValidateRule(language, rule, i);

				// add a visually significant separator between rules to ease debugging
				builder.AppendLine();
				builder.AppendLine("|");
				builder.AppendLine();

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

			return compiled;
		}


		private static void ValidateRule(ILanguage language, IRule rule, int ruleNum)
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

			var count = capturePattern.Matches(rule.Pattern).Count;
			if (count != rule.Captures.Count)
			{
				throw new LanguageException(
					string.Format("{0} rule {1} has misalignment captures", language.Name, ruleNum),
					language.Name, ruleNum);
			}

			if (namedPattern.Match(rule.Pattern).Success)
			{
				throw new LanguageException(
					string.Format("{0} rule {1} cannot contain a named group", language.Name, ruleNum),
					language.Name, ruleNum);
			}
		}
	}
}
