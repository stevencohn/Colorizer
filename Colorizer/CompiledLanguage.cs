//************************************************************************************************
// Copyright © 2020 Steven M Cohn.  All rights reserved.
//************************************************************************************************                

namespace River.OneMore.Colorizer
{
	using System.Collections.Generic;
	using System.Text.RegularExpressions;


	internal class CompiledLanguage
	{
		public CompiledLanguage(string name, Regex regex, IList<string> scopes)
		{
			Name = name;
			Regex = regex;
			Scopes = scopes;
		}


		public string Name { get; private set; }


		public Regex Regex { get; private set; }


		public IList<string> Scopes { get; private set; }
	}
}
