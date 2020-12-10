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
			var colorizer = new Colorizer("foo");
			var root = colorizer.Colorize("header\n\tfoo 123\n   // blah\nfooter");

			Console.WriteLine(root.ToString());
		}
	}
}
