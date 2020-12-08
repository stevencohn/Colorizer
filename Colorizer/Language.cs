//************************************************************************************************
// Copyright © 2020 Steven M Cohn.  All rights reserved.
//************************************************************************************************                

namespace River.OneMore.Colorizer
{
	using System.Collections.Generic;


	/// <summary>
	/// Defines how to parse the source code of a given language
	/// </summary>
	internal interface ILanguage
	{
		/// <summary>
		/// Gets the friendly name of the language
		/// </summary>
		string Name { get; }


		/// <summary>
		/// Gets the pattern of the first line of a source file. Can be null
		/// </summary>
		string PreamblePattern { get; }


		/// <summary>
		/// Get the list of rules that define the language
		/// </summary>
		List<ILanguageRule> Rules { get; }
	}


	/// <summary>
	/// Used only for deserialization; the interface is used thereafter
	/// </summary>
	internal class Language : ILanguage
	{
		public Language()
		{
		}


		public string Name { get; set; }


		public string PreamblePattern { get; set; }


		public List<ILanguageRule> Rules { get; set; }
	}
}
