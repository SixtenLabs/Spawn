using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using SF = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace SixtenLabs.Spawn.CSharp.Extensions
{
  /// <summary>
  /// ArgumentDefinition
  /// </summary>
  public static partial class DefinitionExtensions
  {
    /// <summary>
    /// TODO: Add more logic to cover the nuances of arguments. colon identifiers, etc.
    /// </summary>
    /// <param name="argumentDefinition"></param>
    /// <returns></returns>
    public static ArgumentSyntax CreateArgument(this ArgumentDefinition argumentDefinition)
    {
      var name = SF.IdentifierName(argumentDefinition.Name.Code);

      return SF.Argument(name);
    }

    public static ArgumentListSyntax GetArgumentList(this IList<ArgumentDefinition> argumentDefintions)
    {
      var list = new List<ArgumentSyntax>();

      foreach (var argument in argumentDefintions)
      {
        var argumentNode = CreateArgument(argument);
        list.Add(argumentNode);
      }

      var argumentList = SF.ArgumentList().AddArguments(list.ToArray());

      return argumentList;
    }
  }
}
