﻿
namespace ColorizerTests
{
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using River.OneMoreAddIn.Colorizer;
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Reflection;


	[TestClass]
	public class CompilerTests
	{
		[TestMethod]
		public void CompilerTest()
		{
			var path = Path.Combine(
				Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
				@"Languages\foo.json");

			var language = Provider.LoadLanguage(path);

			Assert.IsNotNull(language);
			Assert.AreEqual(language.Name, "Foo");
			Assert.IsTrue(language.Rules.Count > 0);
			Assert.IsTrue(language.Rules[0].Captures.Count > 0);
			Assert.AreEqual(language.Rules[0].Captures[0], "comment");

			var compiled = Compiler.Compile(language);

			Assert.IsNotNull(compiled.Regex);
			Assert.IsNotNull(compiled.Scopes);
			Assert.IsTrue(compiled.Scopes.Count > 0);

			Console.WriteLine(compiled.Regex.ToString());

			var parser = new Parser(compiled);
			parser.Parse("foo 123\n// blah", (code, scope) =>
			{
				Console.WriteLine($"{scope ?? "SPACE"}: [{code}]");
			});
		}


		[TestMethod]
		[ExpectedException(typeof(LanguageException))]
		public void NamedTest()
		{
			var language = new Language
			{
				Name = "foo",
				Rules = new List<IRule>
				{
					new Rule
					{
						Pattern = @"\\b(?<keyword>foo|bar)\\b",
						Captures = new List<string> { "Keyword" }
					}
				}
			};

			var compiled = Compiler.Compile(language);
		}
	}
}
