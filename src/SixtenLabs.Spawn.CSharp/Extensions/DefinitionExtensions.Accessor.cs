using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SF = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;

namespace SixtenLabs.Spawn.CSharp.Extensions
{
  public static partial class DefinitionExtensions
  {
    private static AccessorDeclarationSyntax ComposeAccessor(AccessorDefinition definition)
    {
      var declaration = SF.AccessorDeclaration((SyntaxKind)definition.AccessorType);

      var modifiers = SF.TokenList();

      foreach (var modifier in definition.ModifierDefinitions)
      {
        var modifierToken = SF.Token((SyntaxKind)modifier.Modifier);
        modifiers.Add(modifierToken);
      }

      if (definition.ModifierDefinitions.Count > 0)
      {
        declaration = declaration.WithModifiers(modifiers);
      }

      if (definition.BlockDefinition != null)
      {
        var block = definition.BlockDefinition.CreateBlock();
        declaration = declaration.WithBody(block);
      }
      else
      {
        declaration = declaration
        .WithSemicolonToken(SF.Token(SyntaxKind.SemicolonToken));
      }

      return declaration;
    }

    private static AccessorListSyntax GetAccessors(AccessorDefinition getter, AccessorDefinition setter)
    {
      var accessorList = SF.AccessorList();

      if (getter != null)
      {
        accessorList = accessorList.AddAccessors(ComposeAccessor(getter));
      }

      if (setter != null)
      {
        accessorList = accessorList.AddAccessors(ComposeAccessor(setter));
      }

      return accessorList;
    }
  }
}
