using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SF = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using System.Linq;
using System;

namespace SixtenLabs.Spawn.CSharp.Extensions
{
	public static partial class SyntaxExtensions
	{
		

		public static CompilationUnitSyntax AddEnum(this CompilationUnitSyntax compilationUnit, OutputDefinition outputDefinition, EnumDefinition enumDefinition)
		{
			if(string.IsNullOrEmpty(enumDefinition.Name.Code))
			{
				throw new ArgumentNullException("Enum must at least have a valid SpecName property set.");
			}

			var nameSpaceDeclaration = outputDefinition.Namespace.CreateNamespaceDeclaration();
      
      var enumDeclaration = enumDefinition.CreateEnumDeclaration();
      
			if (nameSpaceDeclaration != null)
			{
				nameSpaceDeclaration = nameSpaceDeclaration.AddMembers(enumDeclaration);

				return compilationUnit.AddMembers(nameSpaceDeclaration);
			}
			else
			{
				return compilationUnit.AddMembers(enumDeclaration);
			}
		}
	}
}
