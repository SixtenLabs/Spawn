using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using SF = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace SixtenLabs.Spawn.CSharp
{
	public static partial class SyntaxExtensions
	{
		public static CompilationUnitSyntax AddStruct(this CompilationUnitSyntax compilationUnit, OutputDefinition outputDefinition, StructDefinition structDefinition)
		{
			if (string.IsNullOrEmpty(structDefinition.SpecName))
			{
				throw new ArgumentNullException("Struct must at least have a valid SpecName property set.");
			}

			var memberList = SF.List<MemberDeclarationSyntax>();

			var fields = AddFields(structDefinition.Fields);
			var constructors = AddConstructors(structDefinition.Constructors);
			memberList = memberList.AddRange(constructors);
			memberList = memberList.AddRange(fields);

			var nameSpaceDeclaration = AddNamespace(outputDefinition.Namespace);
			var modifierTokens = GetModifierTokens(structDefinition.ModifierDefinitions);
			var attributes = GetAttributes(structDefinition.Attributes);

			if(structDefinition.Comments.HasComments)
			{
				var comments = structDefinition.Comments.GetComments();
				modifierTokens.Insert(0, SF.Token(comments, SyntaxKind.XmlTextLiteralToken, SF.TriviaList()));
			}

			var structDeclaration = SF.StructDeclaration(structDefinition.TranslatedName)
				.WithModifiers(modifierTokens)
				.WithMembers(memberList);

			if(structDefinition.Attributes.Count > 0)
			{
				structDeclaration = structDeclaration.WithAttributeLists(SF.SingletonList(attributes));
			}

			if (nameSpaceDeclaration != null)
			{
				nameSpaceDeclaration = nameSpaceDeclaration.AddMembers(structDeclaration);

				return compilationUnit.AddMembers(nameSpaceDeclaration);
			}
			else
			{
				return compilationUnit.AddMembers(structDeclaration);
			}
		}
	}
}
