//************************************************************************************************
// Copyright © 2020 Steven M Cohn.  All rights reserved.
//************************************************************************************************                

namespace River.OneMore.Colorizer
{
	using System;
	using System.IO;
	using System.Text.Json;
	using System.Text.Json.Serialization;


	internal class LanguageProvider
	{
		public static ILanguage Read(string path)
		{
			var serializeOptions = new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
				ReadCommentHandling = JsonCommentHandling.Skip,
				AllowTrailingCommas = true,

				Converters =
				{
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
			return JsonSerializer.Deserialize<LanguageRule>(ref reader, options);
		}

		public override void Write(
			Utf8JsonWriter writer, ILanguageRule value, JsonSerializerOptions options)
		{
			throw new NotImplementedException();
		}
	}
}
