using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SF = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using System.Collections.Generic;

namespace SixtenLabs.Spawn.CSharp.Extensions
{
  /// <summary>
  /// FieldDefinition Extensions
  /// </summary>
  public static partial class DefinitionExtensions
  {
    public static FieldDeclarationSyntax CreateFieldDeclaration(this FieldDefinition fieldDefinition)
    {
      if (string.IsNullOrEmpty(fieldDefinition.ReturnType.Code))
      {
        throw new ArgumentNullException("Field must have a return type.");
      }

      var modifiers = fieldDefinition.ModifierDefinitions.GetModifierTokens();

      var returnTypeString = fieldDefinition.ReturnType.Code;
      var returnType = SF.VariableDeclaration(SF.IdentifierName(returnTypeString));
      var fieldName = SF.VariableDeclarator(SF.Identifier(fieldDefinition.Name.Code));
      var initializer = fieldDefinition.DefaultValue.GetInitializer();
      var attributes = fieldDefinition.AttributeDefinitions.GetAttributeDeclarations();

      fieldName = fieldName.WithInitializer(initializer);

      var variableDeclaration = returnType.AddVariables(fieldName);

      var fieldDeclaration = SF.FieldDeclaration(variableDeclaration)
        .WithModifiers(modifiers);

      if (fieldDefinition.AttributeDefinitions.Count > 0)
      {
        fieldDeclaration = fieldDeclaration.WithAttributeLists(SF.SingletonList(attributes));
      }

      return fieldDeclaration;
    }

    public static IList<FieldDeclarationSyntax> GetFieldDeclarations(this IList<FieldDefinition> fieldDefintions)
    {
      var fields = new List<FieldDeclarationSyntax>();

      foreach(var fieldDefintion in fieldDefintions)
      {
        var fieldDeclaration = fieldDefintion.CreateFieldDeclaration();
        fields.Add(fieldDeclaration);
      }

      return fields;
    }
  }
}
