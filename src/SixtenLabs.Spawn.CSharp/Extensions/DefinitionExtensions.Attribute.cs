using Microsoft.CodeAnalysis.CSharp.Syntax;
using SF = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using System.Collections.Generic;

namespace SixtenLabs.Spawn.CSharp.Extensions
{
  public static partial class DefinitionExtensions
  {
    public static AttributeSyntax CreateAttributeDeclaration(this AttributeDefinition attributeDefinition)
    {
      var arguments = new List<AttributeArgumentSyntax>();

      foreach (var arg in attributeDefinition.ArgumentDefinitions)
      {
        var argument = SF.AttributeArgument(SF.IdentifierName(arg.Name.Code));
        arguments.Add(argument);
      }

      var attributeArgList = SF.AttributeArgumentList(SF.SeparatedList(arguments));

      var declaration = SF.Attribute(SF.IdentifierName(attributeDefinition.Name.Code), attributeArgList);

      return declaration;
    }

    public static AttributeListSyntax GetAttributeDeclarations(this IList<AttributeDefinition> definitions)
    {
      var list = new List<AttributeSyntax>();

      foreach (var attribute in definitions)
      {
        var attributeSyntax = CreateAttributeDeclaration(attribute);
        list.Add(attributeSyntax);
      }

      return SF.AttributeList(SF.SeparatedList(list));
    }
  }
}
