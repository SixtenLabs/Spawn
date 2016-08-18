using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using SF = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace SixtenLabs.Spawn.CSharp.Extensions
{
  public static partial class DefinitionExtensions
  {
    public static StructDeclarationSyntax CreateStructDeclaration(this StructDefinition structDefinition)
    {
      if (string.IsNullOrEmpty(structDefinition.SpecName))
      {
        throw new ArgumentNullException("Struct must at least have a valid SpecName property set.");
      }

      var memberList = SF.List<MemberDeclarationSyntax>();

      var fields = structDefinition.Fields.GetFieldDeclarations();
      var constructors = structDefinition.Constructors.GetConstructorDeclarations();
      memberList = memberList.AddRange(fields);
      memberList = memberList.AddRange(constructors);

      var modifierTokens = GetModifierTokens(structDefinition.ModifierDefinitions);
      var attributes = structDefinition.Attributes.GetAttributeDeclarations();

      if (structDefinition.Comments.HasComments)
      {
        var comments = structDefinition.Comments.GetCommentTriviaSyntax();
        modifierTokens.Insert(0, SF.Token(comments, SyntaxKind.XmlTextLiteralToken, SF.TriviaList()));
      }

      var structDeclaration = SF.StructDeclaration(structDefinition.TranslatedName)
        .WithModifiers(modifierTokens)
        .WithMembers(memberList);

      if (structDefinition.Attributes.Count > 0)
      {
        structDeclaration = structDeclaration.WithAttributeLists(SF.SingletonList(attributes));
      }

      return structDeclaration;
    }
  }
}
