using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SF = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using Microsoft.CodeAnalysis;

namespace SixtenLabs.Spawn.CSharp.Extensions
{
  public static partial class DefinitionExtensions
  {
    public static EnumMemberDeclarationSyntax CreateEnumMemberDeclaration(this EnumMemberDefinition enumMemberDefition)
    {
      var enumDeclaration = SF.EnumMemberDeclaration(enumMemberDefition.Name.Code);

      var leadingTrivia = enumMemberDefition.CommentDefinition.GetCommentTriviaSyntax();
      var literal = SF.Literal(SF.TriviaList(), enumMemberDefition.Value, 0, SF.TriviaList());
      var equalsValueClause = SF.EqualsValueClause(SF.LiteralExpression(SyntaxKind.NumericLiteralExpression, literal));

      enumDeclaration = enumDeclaration.WithLeadingTrivia(leadingTrivia).WithEqualsValue(equalsValueClause);

      return enumDeclaration;
    }
  }
}
