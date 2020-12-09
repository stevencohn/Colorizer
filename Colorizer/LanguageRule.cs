//************************************************************************************************
// Copyright © 2020 Steven M Cohn.  All rights reserved.
//************************************************************************************************                

namespace River.OneMoreAddIn.Colorizer
{
	using System.Collections.Generic;


	/// <summary>
	/// Defines a single rule for the language such as comments, string, or keywords.
	/// </summary>
	internal interface ILanguageRule
	{
		/// <summary>
		/// Gets a regular expression that defines what the rule matches and captures
		/// </summary>
		string Pattern { get; }


		/// <summary>
		/// Gets the scope of each capture in the regular expression
		/// </summary>
		IList<string> Captures { get; }

	}


	/// <summary>
	/// Used only for deserialization; the interface is used thereafter
	/// </summary>
	internal class LanguageRule : ILanguageRule
	{
		public LanguageRule()
		{
		}


		public string Pattern { get; set; }


		public IList<string> Captures { get; set; }
	}
}
