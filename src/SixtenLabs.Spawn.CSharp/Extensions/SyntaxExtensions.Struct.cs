using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using SF = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace SixtenLabs.Spawn.CSharp.Extensions
{
	public static partial class SyntaxExtensions
	{
		public static CompilationUnitSyntax AddStruct(this CompilationUnitSyntax compilationUnit, OutputDefinition outputDefinition, StructDefinition structDefinition)
		{
			var nameSpaceDeclaration = outputDefinition.Namespace.CreateNamespaceDeclaration();

      var structDeclaration = structDefinition.CreateStructDeclaration();

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
