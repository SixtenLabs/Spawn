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
      
      if (structDefinition.DocumentationCommentDefinition.CommentDefinitions.IsNotEmpty && structDefinition.AttributeDefinitions.IsEmpty)
      {
        var comments = structDefinition.DocumentationCommentDefinition.CommentDefinitions.GetCommentTriviaSyntax();
        modifierTokens = modifierTokens.Insert(0, SF.Token(comments, SyntaxKind.XmlTextLiteralToken, SF.TriviaList()));
      }

      var structDeclaration = SF.StructDeclaration(structDefinition.Name.Output)
        .WithModifiers(modifierTokens)
        .WithMembers(memberList);

      if (structDefinition.AttributeDefinitions.IsNotEmpty)
      {
        var attributes = structDefinition.AttributeDefinitions.GetAttributeDeclarations();

        // How in the hell do I get the comments in the right place?

        var attributesList = SF.SingletonList(attributes);

        //if (structDefinition.CommentDefinitions.IsNotEmpty)
        //{
        //  var comments = structDefinition.CommentDefinitions.GetCommentTriviaSyntax();
        //  attributesList = attributesList.Insert(0, comments);

        //}

        structDeclaration = structDeclaration.WithAttributeLists(attributesList);
      }

      

      return structDeclaration;
    }
  }
}
