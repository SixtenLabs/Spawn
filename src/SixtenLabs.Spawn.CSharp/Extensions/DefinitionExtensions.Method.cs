using Microsoft.CodeAnalysis.CSharp.Syntax;
using SF = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;

namespace SixtenLabs.Spawn.CSharp.Extensions
{
  public static partial class DefinitionExtensions
  {
    public static MethodDeclarationSyntax CreateMethodDeclaration(this MethodDefinition methodDefinition)
    {
      var modifiers = GetModifierTokens(methodDefinition.ModifierDefinitions);
      var returnType = SF.ParseTypeName(methodDefinition.ReturnType.Output);
      var parameters = methodDefinition.ParameterDefinitions.GetParameterDeclarations();
      

      var declaration = SF.MethodDeclaration(returnType, SF.Identifier(methodDefinition.Name.Output))
        .WithModifiers(modifiers)
        .WithParameterList(parameters);

      if(methodDefinition.BlockDefinition.IsEmpty)
      {
        declaration = declaration.WithSemicolonToken(SF.Token(SyntaxKind.SemicolonToken));
      }
      else
      {
        var body = methodDefinition.BlockDefinition.CreateBlock();
        declaration = declaration.WithBody(body);
      }

      if(methodDefinition.AttributeDefinitions.IsNotEmpty)
      {
        var attributes = methodDefinition.AttributeDefinitions.GetAttributeDeclarations();

        declaration = declaration.WithAttributeLists(SF.SingletonList(attributes));
      }

      return declaration;
    }

    public static IList<MethodDeclarationSyntax> GetMethodDeclarations(this IList<MethodDefinition> definitions)
    {
      var list = new List<MethodDeclarationSyntax>();

      foreach (var method in definitions)
      {
        var methodDeclaration = CreateMethodDeclaration(method);
        list.Add(methodDeclaration);
      }

      return list;
    }
  }
}
