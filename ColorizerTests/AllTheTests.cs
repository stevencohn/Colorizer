
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

			var compiled = LanguageCompiler.Compile(language);

			Assert.IsNotNull(compiled);
			Assert.AreEqual(compiled.Name, language.Name);
			Assert.IsNotNull(compiled.Regex);
			Assert.IsNotNull(compiled.Scopes);
			Assert.IsTrue(compiled.Scopes.Count > 0);

			Console.WriteLine(compiled.Regex.ToString());
		}
	}
}
