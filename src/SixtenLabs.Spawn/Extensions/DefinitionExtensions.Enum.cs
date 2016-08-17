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
		public static SyntaxNode EnumMemberDeclaration(this EnumMemberDefinition definition, SyntaxGenerator generator)
		{
			var left = generator.LiteralExpression(definition.TranslatedName);
			var right = generator.LiteralExpression(definition.Value);
			//var expression = generator.AssignmentStatement(null, right);

			var classDeclaration = generator.EnumMember(definition.TranslatedName, right);

			//if (definition.Comments.Count > 0)
			//{
			//	var x = generator.trivia
			//	var x = classDeclaration.WithLeadingTrivia();
			//}

			return classDeclaration;
		}

		public static IEnumerable<SyntaxNode> MemberDeclarations(this EnumDefinition definition, SyntaxGenerator generator)
		{
			var declarations = new List<SyntaxNode>();

			foreach (var member in definition.Members)
			{
				var memberDeclaration = member.EnumMemberDeclaration(generator);
				declarations.Add(memberDeclaration);
			}

			return declarations;
		}

		public static SyntaxNode EnumDeclaration(this EnumDefinition definition, SyntaxGenerator generator)
		{
			var members = new List<SyntaxNode>();

			var enumMembers = definition.MemberDeclarations(generator);
			members.AddRange(enumMembers);

			var enumDeclaration = generator.EnumDeclaration(definition.TranslatedName, definition.AccessModifier, definition.DeclarationModifiers, members);

			if (definition.IsFlags)
			{
				var isFlags = generator.Attribute("Flags");
				enumDeclaration = generator.AddAttributes(enumDeclaration, isFlags);
			}

			return enumDeclaration;
		}
	}
}
