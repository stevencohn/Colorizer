//************************************************************************************************
// Copyright © 2020 Steven M Cohn.  All rights reserved.
//************************************************************************************************                

namespace River.OneMoreAddIn.Colorizer
{
	using System;
	using System.IO;
	using System.Text.Json;
	using System.Text.Json.Serialization;


	internal class LanguageProvider
	{

		/// <summary>
		/// Loads a language from the given file path
		/// </summary>
		/// <param name="path">The path to the language json definition file</param>
		/// <returns>An ILanguage describing the langauge</returns>
		public static ILanguage Read(string path)
		{
			var serializeOptions = new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
				ReadCommentHandling = JsonCommentHandling.Skip,
				AllowTrailingCommas = true,

				Converters =
				{
					// handles interface->class conversion
					new RuleConverter()
				}
			};

			var json = File.ReadAllText(path);
			var language = JsonSerializer.Deserialize<Language>(json, serializeOptions);

			return language;
		}
	}


	internal class RuleConverter: JsonConverter<ILanguageRule>
	{
		public override ILanguageRule Read(
			ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			// convert from ILanguageRule to LanguageRule
			return JsonSerializer.Deserialize<LanguageRule>(ref reader, options);
		}

		public override void Write(
			Utf8JsonWriter writer, ILanguageRule value, JsonSerializerOptions options)
		{
			// we're not serializing so this isn't used
			throw new NotImplementedException();
		}
	}
}
