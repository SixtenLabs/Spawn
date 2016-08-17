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
		public static IEnumerable<SyntaxNode> ParameterDeclarations(this ConstructorDefinition definition, SyntaxGenerator generator)
		{
			var declarations = new List<SyntaxNode>();

			foreach(var parameter in definition.Parameters)
			{
				var parameterDeclaration = parameter.ParameterDeclaration(generator);
				declarations.Add(parameterDeclaration);
			}

			return declarations;
		}

		public static SyntaxNode ConstructorDeclaration(this ConstructorDefinition constructorDefinition, SyntaxGenerator generator)
		{
			var parameters = constructorDefinition.ParameterDeclarations(generator);

			var constructorDeclaration = generator.ConstructorDeclaration(constructorDefinition.TranslatedName, parameters, constructorDefinition.AccessModifier, constructorDefinition.DeclarationModifiers, null, null);

			return constructorDeclaration;
		}
	}
}
