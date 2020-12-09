
namespace ColorizerTests
{
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using River.OneMore.Colorizer;
	using System;
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

			var interpreter = LanguageCompiler.Compile(language);

			Assert.IsNotNull(interpreter);
			Assert.AreEqual(interpreter.Name, language.Name);
			Assert.IsNotNull(interpreter.Regex);
			Assert.IsNotNull(interpreter.Scopes);
			Assert.IsTrue(interpreter.Scopes.Count > 0);

			Console.WriteLine(interpreter.Regex.ToString());

			var parser = new LanguageParser(interpreter);
			parser.Parse(" foo 123 ", (code, scope) =>
			{
				Console.WriteLine($"{code} ({scope})");
			});
		}
	}
}
