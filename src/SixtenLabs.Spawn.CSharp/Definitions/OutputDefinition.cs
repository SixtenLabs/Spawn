using System.Collections.Generic;

namespace SixtenLabs.Spawn.CSharp
{
	/// <summary>
	/// Use this class to define an output.
	/// </summary>
	public class OutputDefinition : IOutputDefinition
	{
		public OutputDefinition()
		{
		}

		/// <summary>
		/// Add the names of the dlls to use to create using statements for this output
		/// </summary>
		/// <param name="names"></param>
		public void AddStandardUsingDirective(string dllName)
		{
			var usingDefinition = new UsingDirectiveDefinition(dllName);
			Usings.Add(usingDefinition);
		}

		/// <summary>
		/// Add the names of the dlls to use to create using statements for this output
		/// </summary>
		/// <param name="names"></param>
		public void AddStaticUsingDirective(string dllName)
		{
			var usingDefinition = new UsingDirectiveDefinition(dllName) { IsStatic = true };
			Usings.Add(usingDefinition);
		}

		/// <summary>
		/// Add the names of the dlls to use to create using statements for this output
		/// </summary>
		/// <param name="names"></param>
		public void AddAliasedUsingDirective(string dllName, string alias)
		{
			var usingDefinition = new UsingDirectiveDefinition(dllName) { Alias = alias, UseAlias = true };
			Usings.Add(usingDefinition);
		}

		public void AddNamespace(string @namespace)
		{
			Namespace = new NamespaceDefinition(@namespace);
		}


		/// <summary>
		/// The names of the dlls to use to create using statements for this output
		/// </summary>
		public IList<UsingDirectiveDefinition> Usings { get; } = new List<UsingDirectiveDefinition>();

		/// <summary>
		/// The namesapce of the output.
		/// </summary>
		public NamespaceDefinition Namespace { get; set; }

		public string TargetSolution { get; set; }

		public string FileName { get; set; }

    public string Extension { get; set; }

		public IList<string> CommentLines { get; set; } = new List<string>();

		public string OutputDirectory { get; set; }
	}
}
