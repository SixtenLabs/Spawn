using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Spawn
{
	/// <summary>
	/// Field Definition Extensions
	/// </summary>
	public static partial class DefinitionExtensions
	{
		public static SyntaxNode FieldDeclaration(this FieldDefinition definition, SyntaxGenerator generator)
		{
			var fieldType = generator.TypeExpression(definition.FieldType);

			// TODO : Going to need extensions for expressions by type. So will need ExpressionDefinition
			// = 0;
			// = new WindowHandle();
			// = "string"
			// etc...
			var initializer = generator.LiteralExpression(definition.Initializer);

			var declaration = generator.FieldDeclaration(definition.TranslatedName, fieldType, definition.AccessModifier, definition.DeclarationModifiers, initializer);

			return declaration;
		}
	}
}
