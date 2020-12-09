//************************************************************************************************
// Copyright © 2020 Steven M Cohn.  All rights reserved.
//************************************************************************************************                

namespace River.OneMore.Colorizer
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;


    internal static class LanguageCompiler
	{
        private static readonly Regex captureCountRegex = 
            new Regex(@"(?x)(?<!(\\|(?!\\)\(\?))\((?!\?)", RegexOptions.Compiled);


        public static CompiledLanguage Compile(ILanguage language)
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

            return new CompiledLanguage(
                language.Name,
                new Regex(builder.ToString()),
                scopes);
        }


        private static void ValidateRule(ILanguage language, ILanguageRule rule, int i)
		{
            if (string.IsNullOrWhiteSpace(rule.Pattern))
			{
                throw new ArgumentNullException(
                    $"empty pattern in {language.Name} rule {i}");
			}

            if (rule.Captures == null || rule.Captures.Count == 0)
			{
                throw new ArgumentOutOfRangeException(
                    $"no captures defined in {language.Name} rule {i}");
			}

            var count = captureCountRegex.Matches(rule.Pattern).Count;
            if (count != rule.Captures.Count)
            {
                throw new DataMisalignedException(
                    $"Capture misalignment in {language.Name} rule {i}");
            }
        }
    }
}
