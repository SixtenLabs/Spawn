using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using SF = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using System.Collections.Generic;

namespace SixtenLabs.Spawn.CSharp.Extensions
{
  /// <summary>
  /// ModifierDefinition Extensions
  /// </summary>
  public static partial class DefinitionExtensions
  {
    public static SyntaxTokenList GetModifierTokens(this IList<ModifierDefinition> modifierDefinitions)
    {
      var list = new List<SyntaxToken>();

      foreach (var modifier in modifierDefinitions)
      {
        list.Add(SF.Token((SyntaxKind)modifier.Modifier));
      }

      return SF.TokenList(list);
    }
  }
}
