using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Spawn
{
	public static partial class DefinitionExtensions
	{
		public static IList<SyntaxNode> UsingDirectiveDeclarations(this OutputDefinition outputDefinition, SyntaxGenerator generator)
		{
			var usingDirectives = new List<SyntaxNode>();

			foreach (var usingDirective in outputDefinition.Usings)
			{
				var directive = generator.NamespaceImportDeclaration(usingDirective.SpecName);
				usingDirectives.Add(directive);
			}

			return usingDirectives;
		}
	}
}
