
namespace ColorizerTests
{
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using River.OneMoreAddIn.Colorizer;
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Reflection;


	[TestClass]
	public class AllTheTests
	{
		[TestMethod]
		public void AllTests()
		{
			var path = Path.Combine(
				Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
				@"Languages\foo.json");
			
			var language = LanguageProvider.Read(path);

			Assert.IsNotNull(language);
			Assert.AreEqual(language.Name, "Foo");
			Assert.IsTrue(language.Rules.Count > 0);
			Assert.IsTrue(language.Rules[0].Captures.Count > 0);
			Assert.AreEqual(language.Rules[0].Captures[0], "Comment");

			var compiled = LanguageCompiler.Compile(language);

			Assert.IsNotNull(compiled.Regex);
			Assert.IsNotNull(compiled.Scopes);
			Assert.IsTrue(compiled.Scopes.Count > 0);

			Console.WriteLine(compiled.Regex.ToString());

			var parser = new LanguageParser(compiled);
			parser.Parse("foo bar 123 88\n// adsfl kas falksfd", (code, scope) =>
			{
				Console.WriteLine($"'{code}' ({scope})");
			});
		}


		[TestMethod]
		[ExpectedException(typeof(LanguageException))]
		public void NamedTes()
		{
			var language = new Language
			{
				Name = "foo",
				Rules = new List<ILanguageRule>
				{
					new LanguageRule
					{
						Pattern = @"\\b(?<keyword>foo|bar)\\b",
						Captures = new List<string> { "Keyword" }
					}
				}
			};

			var compiled = LanguageCompiler.Compile(language);
		}
	}
}
