using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Spawn
{
	public static partial class DefinitionExtension
	{
		public static SyntaxNode ParameterDeclaration(this ParameterDefinition definition, SyntaxGenerator generator)
		{
			var declaration = generator.ParameterDeclaration(definition.TranslatedName, null, null, definition.RefKind);

			return declaration;
		}

		
	}
}
