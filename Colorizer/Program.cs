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
			CSS();
			//HTML();
			//JavaScript();
			//PowerShell();
			//Typescript();
			//XML();
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


		static void CSS()
		{
			Console.WriteLine();
			Console.WriteLine("css------------------------------");

			var colorizer = new Colorizer("css");

			var one = colorizer.ColorizeOne(
@"h1 {
  color: white;
  text-align: center;
  /* comment */
}");

			Console.WriteLine(one);
		}


		static void HTML()
		{
			Console.WriteLine();
			Console.WriteLine("html-----------------------------");

			var colorizer = new Colorizer("html");

			var one = colorizer.ColorizeOne(
@"<!DOCTYPE html>
<html>
<!--comment-->
<div style=""color:blue;"">whatever &quot;To be&quot;</div>
</html>");

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


		static void PowerShell()
		{
			Console.WriteLine();
			Console.WriteLine("powershell-----------------------");

			var colorizer = new Colorizer("powershell");

			var one = colorizer.ColorizeOne(
"if ($rule.Enabled -eq $true) { Write-Host(\"Found enabled rule {0}\" -f $rule.DisplayName); }");
 
			Console.WriteLine(one);
		}


		static void Typescript()
		{
			Console.WriteLine();
			Console.WriteLine("typescript-----------------------");

			var colorizer = new Colorizer("typescript");

			var one = colorizer.ColorizeOne(
@"async onClickEvent(event: MouseEvent): Promise<void>
{
  if (this.stopEventPropagation)
  {
    event.stopPropagation();
  }
  await this.buildAndRecord(event.type, event.target);
}");
			Console.WriteLine(one);
		}


		static void XML()
		{
			Console.WriteLine();
			Console.WriteLine("xml------------------------------");

			var colorizer = new Colorizer("xml");

			var one = colorizer.ColorizeOne(
@"<?xml version=""1.0"" standalone=""no"" ?>
<!DOCTYPE document SYSTEM ""subjects.dtd"">
<!--comment-->
<foo>
  <bar a=""123"">whatever</bar>
  <const><![CDATA[value]]></const>
</foo>");

			Console.WriteLine(one);
		}
	}
}
