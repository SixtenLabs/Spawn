using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Spawn.Extensions
{
	public static partial class DefinitionExtensions
	{
		public static SyntaxNode UsingDeclaration(this UsingDirectiveDefinition usingDirectiveDefinition, SyntaxGenerator generator)
		{
			var usingDirectives = generator.NamespaceImportDeclaration(usingDirectiveDefinition.SpecName);

			return usingDirectives;
		}

		
	}
}
