//************************************************************************************************
// Copyright © 2020 Steven M Cohn.  All rights reserved.
//************************************************************************************************                

namespace Colorizer
{
	using River.OneMoreAddIn.Colorizer;
	using System;
	using System.Xml.Linq;


	class Program
	{
		static void Main(string[] args)
		{
			var colorizer = new Colorizer("csharp");

			var one = colorizer.Colorize(
@"public void Foobar(int arg)
{
    // comment

    var s = ""bubble"";

    /*
     * also a comment
     */
}", (XNamespace)"one");

			//Console.WriteLine("<html>");
			//Console.WriteLine("<body>");

			Console.WriteLine(one);

			//Console.WriteLine("</body>");
			//Console.WriteLine("</html>");
		}
	}
}
