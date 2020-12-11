//************************************************************************************************
// Copyright © 2020 Steven M Cohn.  All rights reserved.
//************************************************************************************************                

namespace Colorizer
{
	using River.OneMoreAddIn.Colorizer;
	using System;


	class Program
	{
		static void Main(string[] args)
		{
			//Foo();
			//CSharp();
			//JavaScript();
			XML();
		}


		static void Foo()
		{
			Console.WriteLine();
			Console.WriteLine("foo------------------------------");

			var colorizer = new Colorizer("foo");

			var root = colorizer.Colorize("header\n\tfoo 123\n   // blah\nfooter");
			Console.WriteLine(root.ToString());

			var one = colorizer.ColorizeOne("foo\n123");
			Console.WriteLine(one);
		}


		static void CSharp()
		{
			Console.WriteLine();
			Console.WriteLine("csharp---------------------------");

			var colorizer = new Colorizer("csharp");

			var one = colorizer.ColorizeOne(
				"public void Foobar(int arg)\n{\n    var s = \"bubble\";\n}");

			Console.WriteLine(one);
		}


		static void JavaScript()
		{
			Console.WriteLine();
			Console.WriteLine("javascript-----------------------");

			var colorizer = new Colorizer("javascript");

			var one = colorizer.ColorizeOne(
				"var foo;\nfor (x in obj)\n{\n  console.out(x);\n}");

			Console.WriteLine(one);
		}


		static void XML()
		{
			Console.WriteLine();
			Console.WriteLine("xml------------------------------");

			var colorizer = new Colorizer("xml");

			var one = colorizer.ColorizeOne(
"<!--comment-->\n<foo>\n  <bar a=\"123\">whatever</bar>\n  <const><![CDATA[value]]></const>\n</foo>");

			Console.WriteLine(one);
		}
	}
}
