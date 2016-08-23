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
      var memberList = SF.List<MemberDeclarationSyntax>();

      var fields = structDefinition.FieldDefinitions.GetFieldDeclarations();
      var constructors = structDefinition.ConstructorDefinitions.GetConstructorDeclarations();
      memberList = memberList.AddRange(fields);
      memberList = memberList.AddRange(constructors);

      var modifierTokens = GetModifierTokens(structDefinition.ModifierDefinitions);
      
      if (structDefinition.Comments.HasComments)
      {
        var comments = structDefinition.Comments.GetCommentTriviaSyntax();
        modifierTokens.Insert(0, SF.Token(comments, SyntaxKind.XmlTextLiteralToken, SF.TriviaList()));
      }

      var structDeclaration = SF.StructDeclaration(structDefinition.Name.Code)
        .WithModifiers(modifierTokens)
        .WithMembers(memberList);

      if (structDefinition.AttributeDefinitions.Count > 0)
      {
        var attributes = structDefinition.AttributeDefinitions.GetAttributeDeclarations();
        structDeclaration = structDeclaration.WithAttributeLists(SF.SingletonList(attributes));
      }

      return structDeclaration;
    }
  }
}
