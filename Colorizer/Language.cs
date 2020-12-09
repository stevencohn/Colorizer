﻿//************************************************************************************************
// Copyright © 2020 Steven M Cohn.  All rights reserved.
//************************************************************************************************                

namespace River.OneMoreAddIn.Colorizer
{
	using System.Collections.Generic;
	using System.Text.RegularExpressions;


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
		/// Gets the pattern of the first line of a source file. Can be null.
		/// Useful for languages such as XML or ASP.NET with a declarative first line
		/// </summary>
		string PreamblePattern { get; }


		/// <summary>
		/// Get the list of rules that define the language
		/// </summary>
		/// <remarks>
		/// This list only exists until the language is compiled and then it is cleared.
		/// </remarks>
		List<ILanguageRule> Rules { get; }
	}


	/// <summary>
	/// Defines the extended properties for a compiled language including its compiled
	/// regular expression and the ordered list of scopes expected in that expression.
	/// </summary>
	internal interface ICompiledLanguage : ILanguage
	{
		/// <summary>
		/// The compiled pattern matching expression, combined from all language rules
		/// </summary>
		Regex Regex { get; }


		/// <summary>
		/// The ordered list of scopes in the compiled regular expression
		/// </summary>
		IList<string> Scopes { get; }
	}


	/// <summary>
	/// Used only for deserialization (and a little sneaky use in the compiler).
	/// The interface is used thereafter.
	/// </summary>
	internal class Language : ICompiledLanguage
	{
		public Language()
		{
		}


		// Definition...

		public string Name { get; set; }


		public string PreamblePattern { get; set; }


		public List<ILanguageRule> Rules { get; set; }


		// Compilation...

		public Regex Regex { get; set; }


		public IList<string> Scopes { get; set; }
	}
}
