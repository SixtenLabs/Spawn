using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SF = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System;

namespace SixtenLabs.Spawn.CSharp.Extensions
{
  public static partial class DefinitionExtensions
  {
    public static List<EnumMemberDeclarationSyntax> GetEnumMembers(this EnumDefinition enumDefinition)
    {
      var enumMembers = new List<EnumMemberDeclarationSyntax>();

      foreach (var enumMember in enumDefinition.Members)
      {
        var enumDeclaration = enumMember.CreateEnumMemberDeclaration();
        enumMembers.Add(enumDeclaration);
      }

      return enumMembers;
    }

    public static EnumDeclarationSyntax CreateEnumDeclaration(this EnumDefinition enumDefinition)
    {
      if (string.IsNullOrEmpty(enumDefinition.Name.Code))
      {
        throw new ArgumentNullException("Enum must at least have a valid SpecName property set.");
      }

      var members = enumDefinition.GetEnumMembers();
      var modifiers = enumDefinition.ModifierDefinitions.GetModifierTokens();
      var attributes = enumDefinition.AttributeDefinitions.GetAttributeDeclarations();

      var enumDeclaration = SF.EnumDeclaration(enumDefinition.Name.Code)
        .WithModifiers(modifiers)
        .WithMembers(SF.SeparatedList(members));

      if (enumDefinition.AttributeDefinitions.Count > 0)
      {
        enumDeclaration = enumDeclaration.WithAttributeLists(SF.SingletonList(attributes));
      }

      if (enumDefinition.BaseType != SyntaxKindDto.None)
      {
        enumDeclaration = enumDeclaration.WithBaseList(SF.BaseList(SF.SingletonSeparatedList<BaseTypeSyntax>(SF.SimpleBaseType(SF.PredefinedType(SF.Token((SyntaxKind)enumDefinition.BaseType))))));
      }

      return enumDeclaration;
    }
  }
}
