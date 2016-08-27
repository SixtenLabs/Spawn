﻿using Microsoft.CodeAnalysis.CSharp.Syntax;
using SF = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;

namespace SixtenLabs.Spawn.CSharp.Extensions
{
  public static partial class DefinitionExtensions
  {
    public static MethodDeclarationSyntax CreateMethodDeclaration(this MethodDefinition methodDefinition)
    {
      var attributes = methodDefinition.AttributeDefinitions.GetAttributeDeclarations();
      var modifiers = GetModifierTokens(methodDefinition.ModifierDefinitions);
      var returnType = SF.ParseTypeName(methodDefinition.ReturnType.Code);
      var parameters = methodDefinition.ParameterDefinitions.GetParameterDeclarations();
      var body = methodDefinition.BlockDefinition.CreateBlock();

      var declaration = SF.MethodDeclaration(returnType, SF.Identifier(methodDefinition.Name.Code))
        .WithAttributeLists(SF.SingletonList(attributes))
        .WithModifiers(modifiers)
        .WithParameterList(parameters)
        .WithBody(body)
        .WithSemicolonToken(SF.Token(SyntaxKind.SemicolonToken));

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
