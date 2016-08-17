using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.CodeAnalysis.MSBuild;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Xsl;

namespace SixtenLabs.Spawn
{
  public class CodeGenerator : ICodeGenerator
  {
    public CodeGenerator(ISpawnService spawn, string language)
    {
			Spawn = spawn;
			Language = language;

			SetGenerator();
    }

		protected void AddToProject(IOutputDefinition outputDefinition, string contents)
		{
			Spawn.AddDocumentToProject(outputDefinition.TargetSolution, outputDefinition.FileName, contents, new[] { outputDefinition.OutputDirectory });
		}

		public void SetGenerator()
		{
			Generator = SyntaxGenerator.GetGenerator(Spawn.Workspace, Language);
		}

		public string GenerateClass(OutputDefinition outputDefinition, ClassDefinition classDefinition)
		{
			var declarations = new List<SyntaxNode>();

			var usingDirectives = outputDefinition.UsingDirectiveDeclarations(Generator);
			declarations.AddRange(usingDirectives);

			var classDeclaration = classDefinition.ClassDeclaration(Generator);

			// Declare a namespace
			var namespaceDeclaration = Generator.NamespaceDeclaration("MyTypes", classDeclaration);

			declarations.Add(namespaceDeclaration);

			// Get a CompilationUnit (code file) for the generated code
			var newNode = Generator.CompilationUnit(declarations).NormalizeWhitespace();

			var code = newNode.GetFormattedCode();

			return code;
		}

		public string GenerateEnum(OutputDefinition outputDefinition, EnumDefinition enumDefinition)
		{
			var declarations = new List<SyntaxNode>();

			var usingDirectives = outputDefinition.UsingDirectiveDeclarations(Generator);
			declarations.AddRange(usingDirectives);

			var enumDeclaration = enumDefinition.EnumDeclaration(Generator);

			// Declare a namespace
			var namespaceDeclaration = Generator.NamespaceDeclaration("MyTypes", enumDeclaration);

			declarations.Add(namespaceDeclaration);

			// Get a CompilationUnit (code file) for the generated code
			var newNode = Generator.CompilationUnit(declarations).NormalizeWhitespace();

			var code = newNode.GetFormattedCode();

			return code;
		}

		protected ISpawnService Spawn { get; }

		public SyntaxGenerator Generator { get; private set; }

		private string Language { get; }
	}
}
