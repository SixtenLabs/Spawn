using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SF = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using System.Collections.Generic;

namespace SixtenLabs.Spawn.CSharp.Extensions
{
  public static partial class DefinitionExtensions
  {
    public static PropertyDeclarationSyntax CreatePropertyDeclaration(this PropertyDefinition propertyDefinition)
    {
      var modifiers = GetModifierTokens(propertyDefinition.ModifierDefinitions);
      var returnType = SF.ParseTypeName(propertyDefinition.ReturnType.Code);
      var accessors = GetAccessors(propertyDefinition.Getter, propertyDefinition.Setter);
      var initializer = propertyDefinition.DefaultValue.GetInitializer();

      var declaration = SF.PropertyDeclaration(returnType, SF.Identifier(propertyDefinition.Name.Code))
        .WithModifiers(modifiers)
        .WithAccessorList(accessors)
        .WithInitializer(initializer);

      return declaration;
    }

    public static IList<PropertyDeclarationSyntax> GetPropertyDeclarations(this IList<PropertyDefinition> definitions)
    {
      var list = new List<PropertyDeclarationSyntax>();

      foreach (var property in definitions)
      {
        var propertyDeclaration = CreatePropertyDeclaration(property);
        list.Add(propertyDeclaration);
      }

      return list;
    }
  }
}
