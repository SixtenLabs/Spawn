﻿using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SixtenLabs.Spawn.CSharp.Extensions
{
	public static partial class SyntaxExtensions
	{
		public static CompilationUnitSyntax AddClass(this CompilationUnitSyntax compilationUnit, OutputDefinition outputDefinition, ClassDefinition classDefinition)
		{
			var nameSpaceDeclaration = outputDefinition.Namespace.CreateNamespaceDeclaration();

      var classDeclaration = classDefinition.CreateClassDeclaration();

			nameSpaceDeclaration = nameSpaceDeclaration.AddMembers(classDeclaration);

			return compilationUnit.AddMembers(nameSpaceDeclaration);
		}
	}
}
