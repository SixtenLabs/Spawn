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
		public static IEnumerable<SyntaxNode> FieldDeclarations(this ClassDefinition definition, SyntaxGenerator generator)
		{
			var declarations = new List<SyntaxNode>();

			foreach (var field in definition.Fields)
			{
				var fieldDeclaration = field.FieldDeclaration(generator);
				declarations.Add(fieldDeclaration);
			}

			return declarations;
		}

		public static IEnumerable<SyntaxNode> ConstructorDeclarations(this ClassDefinition definition, SyntaxGenerator generator)
		{
			var declarations = new List<SyntaxNode>();

			foreach (var constructor in definition.Constructors)
			{
				var constructorDeclaration = constructor.ConstructorDeclaration(generator);
				declarations.Add(constructorDeclaration);
			}

			return declarations;
		}

		public static SyntaxNode ClassDeclaration(this ClassDefinition classDefinition, SyntaxGenerator generator)
		{
			var members = new List<SyntaxNode>();

			var fields = classDefinition.FieldDeclarations(generator);
			members.AddRange(fields);

			var constructors = classDefinition.ConstructorDeclarations(generator);
			members.AddRange(constructors);

			var classDeclaration = generator.ClassDeclaration(classDefinition.TranslatedName, null,
				classDefinition.AccessModifier, classDefinition.DeclarationModifiers, null, null, members);

			return classDeclaration;
		}
	}
}
