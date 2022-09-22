
namespace ColorizerTests
{
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using River.OneMoreAddIn.Colorizer;
	using System;


	[TestClass]
	public class ColorizerTests
	{
		[TestMethod]
		public void CPP()
		{
			Console.WriteLine();
			Console.WriteLine("cpp---------------------------");

			var colorizer = new Colorizer("cpp", "light", false);

			var one = colorizer.ColorizeOne(
				"// simple comment");

			Console.WriteLine(one);
			Console.WriteLine();

			var two = colorizer.ColorizeOne(
@"/* multi
 * line and
 * more
 */");
			two = two.Replace("<br/>", "<br/>\n");

			Console.WriteLine(two);
			Console.WriteLine();
		}


		[TestMethod]
		public void CSharp()
		{
			Console.WriteLine();
			Console.WriteLine("csharp---------------------------");

			var colorizer = new Colorizer("csharp", "light", false);

			var one = colorizer.ColorizeOne(
				"public void Foobar(int arg)\n{\n    var s = \"bubble\";\n}");

			Console.WriteLine(one);
			Console.WriteLine();
		}


		[TestMethod]
		public void CSS()
		{
			Console.WriteLine();
			Console.WriteLine("css------------------------------");

			var colorizer = new Colorizer("css", "light", false);

			var one = colorizer.ColorizeOne(
@"h1 {
  color: white;
  text-align: center;
  /* comment */
}");

			Console.WriteLine(one);
			Console.WriteLine();
		}


		[TestMethod]
		public void HTML()
		{
			Console.WriteLine();
			Console.WriteLine("html-----------------------------");

			var colorizer = new Colorizer("html", "light", false);

			var one = colorizer.ColorizeOne(
@"<!DOCTYPE html>
<html>
<!--comment-->
<div style=""color:blue;"">whatever &quot;To be&quot;</div>
</html>");

			Console.WriteLine(one);
		}


		[TestMethod]
		public void Java()
		{
			Console.WriteLine();
			Console.WriteLine("java-----------------------------");

			var colorizer = new Colorizer("java", "light", false);

			var one = colorizer.ColorizeOne(
@"public public void main(String[] args) {
  float first = 12.0f, second = 24.5f;
}");

			Console.WriteLine(one);
		}


		[TestMethod]
		public void JavaScript()
		{
			Console.WriteLine();
			Console.WriteLine("javascript-----------------------");

			var colorizer = new Colorizer("javascript", "light", false);

			var one = colorizer.ColorizeOne(
				"var foo;\nfor (x in obj)\n{\n  console.out(x);\n}");

			Console.WriteLine(one);
		}


		[TestMethod]
		public void PowerShell()
		{
			Console.WriteLine();
			Console.WriteLine("powershell-----------------------");

			var colorizer = new Colorizer("powershell", "light", false);

			var one = colorizer.ColorizeOne(
"if ($rule.Enabled -eq $true) { Write-Host(\"Found enabled rule {0}\" -f $rule.DisplayName); }");

			Console.WriteLine(one);
		}


		[TestMethod]
		public void Python()
		{
			Console.WriteLine();
			Console.WriteLine("python---------------------------");

			var colorizer = new Colorizer("python", "light", false);

			var one = colorizer.ColorizeOne(
@"# Display the sum
print('The sum of {0} and {1} is {2}'.format(num1, num2, sum))");

			Console.WriteLine(one);
		}


		[TestMethod]
		public void Typescript()
		{
			Console.WriteLine();
			Console.WriteLine("typescript-----------------------");

			var colorizer = new Colorizer("typescript", "light", false);

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


		[TestMethod]
		public void VB()
		{
			Console.WriteLine();
			Console.WriteLine("vb-------------------------------");

			var colorizer = new Colorizer("vb", "light", false);

			var one = colorizer.ColorizeOne(
@"Imports System
  ' comment
  Public Class Hello
   Public Shared Sub Main(  )
    Console.WriteLine(""hello, world"")
   End Sub
End Class");

			Console.WriteLine(one);
		}


		[TestMethod]
		public void XML()
		{
			Console.WriteLine();
			Console.WriteLine("xml------------------------------");

			var colorizer = new Colorizer("xml", "light", false);

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
