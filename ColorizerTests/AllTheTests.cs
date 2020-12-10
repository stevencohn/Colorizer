
namespace ColorizerTests
{
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using River.OneMoreAddIn.Colorizer;
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Reflection;
	using System.Xml.Linq;

	[TestClass]
	public class AllTheTests
	{
		[TestMethod]
		public void CompilerTests()
		{
			var path = Path.Combine(
				Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
				@"Languages\foo.json");
			
			var language = Provider.LoadLanguage(path);

			Assert.IsNotNull(language);
			Assert.AreEqual(language.Name, "Foo");
			Assert.IsTrue(language.Rules.Count > 0);
			Assert.IsTrue(language.Rules[0].Captures.Count > 0);
			Assert.AreEqual(language.Rules[0].Captures[0], "Comment");

			var compiled = Compiler.Compile(language);

			Assert.IsNotNull(compiled.Regex);
			Assert.IsNotNull(compiled.Scopes);
			Assert.IsTrue(compiled.Scopes.Count > 0);

			Console.WriteLine(compiled.Regex.ToString());

			var parser = new Parser(compiled);
			parser.Parse("foo 123\n// blah", (code, scope) =>
			{
				Console.WriteLine($"'{code}' ({scope})");
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


		[TestMethod]
		public void ColorizerTests()
		{
			var colorizer = new Colorizer("foo");
			var root = colorizer.Colorize("header\nfoo 123\n// blah\nfooter");

			Assert.IsNotNull(root);
			Console.WriteLine(root.ToString());
		}
	}
}
