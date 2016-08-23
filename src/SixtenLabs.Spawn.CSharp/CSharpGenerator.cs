using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using SF = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using SixtenLabs.Spawn.CSharp.Extensions;

namespace SixtenLabs.Spawn.CSharp
{
	public class CSharpGenerator : CodeGenerator
	{
		public CSharpGenerator(ISpawnService spawn)
			: base(spawn)
		{
		}

		/// <summary>
		/// Generate a single output with a class
		/// </summary>
		/// <param name="outputDefinition"></param>
		/// <param name="classDefinition"></param>
		/// <returns></returns>
		public string GenerateClass(OutputDefinition outputDefinition, ClassDefinition classDefinition)
		{
			var code = SF.CompilationUnit()
				.AddUsingDirectives(outputDefinition.Usings)
				.AddClass(outputDefinition, classDefinition)
				.WithEndOfFileToken(SF.Token(SyntaxKind.EndOfFileToken))
				.NormalizeWhitespace();

      if (classDefinition.CommentDefinition.HasComments)
      {
        code = code.WithLeadingTrivia(classDefinition.CommentDefinition.GetCommentTriviaSyntax());
      }

      var contents = code.GetFormattedCode();
			AddToProject(outputDefinition, contents);

			return contents;
		}

		/// <summary>
		/// Generate a single output with a class
		/// </summary>
		/// <param name="outputDefinition"></param>
		/// <param name="structDefinition"></param>
		/// <returns></returns>
		public string GenerateStruct(OutputDefinition outputDefinition, StructDefinition structDefinition)
		{
			var code = SF.CompilationUnit()
				.AddUsingDirectives(outputDefinition.Usings)
				.AddStruct(outputDefinition, structDefinition)
				.WithEndOfFileToken(SF.Token(SyntaxKind.EndOfFileToken))
				.NormalizeWhitespace(indentation: "    ");

      if (structDefinition.Comments.HasComments)
      {
        code = code.WithLeadingTrivia(structDefinition.Comments.GetCommentTriviaSyntax());
      }

      var contents = code.GetFormattedCode();
			AddToProject(outputDefinition, contents);

			return contents;
		}

		/// <summary>
		/// Generate a single output with an enum
		/// </summary>
		/// <param name="outputDefinition"></param>
		/// <param name="enumDefinition"></param>
		/// <returns></returns>
		public string GenerateEnum(OutputDefinition outputDefinition, EnumDefinition enumDefinition)
		{
			var code = SF.CompilationUnit()
				.AddUsingDirectives(outputDefinition.Usings)
        .AddEnum(outputDefinition, enumDefinition)
				.WithEndOfFileToken(SF.Token(SyntaxKind.EndOfFileToken))
				.NormalizeWhitespace();

      if(enumDefinition.Comments.HasComments)
      {
        code = code.WithLeadingTrivia(enumDefinition.Comments.GetCommentTriviaSyntax());
      }

			var contents = code.GetFormattedCode();
			AddToProject(outputDefinition, contents);

			return contents;
		}
	}
}
