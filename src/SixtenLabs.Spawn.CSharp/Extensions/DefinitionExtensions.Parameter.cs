using Microsoft.CodeAnalysis.CSharp.Syntax;
using SF = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using System.Collections.Generic;

namespace SixtenLabs.Spawn.CSharp.Extensions
{
  public static partial class DefinitionExtensions
  {
    public static ParameterSyntax CreateParameterDeclaration(this ParameterDefinition definition)
    {
      var typeName = SF.IdentifierName(definition.TranslatedReturnType);

      var token = SF.VerbatimIdentifier(SF.TriviaList(), definition.TranslatedName, "test", SF.TriviaList());

      TypeSyntax typeSyntax = null;

      if (definition.IsPointer)
      {
        // PointerType is hard code here. Need to determine the type from the definition and create appropriate type.
        typeSyntax = SF.PointerType(SF.ParseTypeName(definition.TranslatedReturnType));
      }
      else
      {
        typeSyntax = SF.ParseTypeName(definition.TranslatedReturnType);
      }

      var parameter = SF.Parameter(token).WithType(typeSyntax);

      return parameter;
    }

    private static ParameterListSyntax GetParameterDeclarations(this IList<ParameterDefinition> definitions)
    {
      var list = new List<ParameterSyntax>();

      foreach (var parameter in definitions)
      {
        var parameterDeclaration = CreateParameterDeclaration(parameter);
        list.Add(parameterDeclaration);
      }

      return SF.ParameterList(SF.SeparatedList(list));
    }

  }
}
