using Microsoft.CodeAnalysis.CSharp.Syntax;
using SF = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;

namespace SixtenLabs.Spawn.CSharp.Extensions
{
  public static partial class DefinitionExtensions
  {
    private static UsingDirectiveSyntax StandardUsing(string dllName)
    {
      return SF.UsingDirective(SF.IdentifierName(dllName)).WithUsingKeyword(SF.Token(SyntaxKind.UsingKeyword)).WithSemicolonToken(SF.Token(SyntaxKind.SemicolonToken));
    }

    private static UsingDirectiveSyntax StaticUsing(string dllName)
    {
      return SF.UsingDirective(SF.IdentifierName(dllName)).WithUsingKeyword(SF.Token(SyntaxKind.UsingKeyword)).WithStaticKeyword(SF.Token(SyntaxKind.StaticKeyword)).WithSemicolonToken(SF.Token(SyntaxKind.SemicolonToken));
    }

    private static UsingDirectiveSyntax AliasUsing(string dllName, string alias)
    {
      var aliasNameEqualsSyntax = SF.NameEquals(SF.IdentifierName(alias));
      return SF.UsingDirective(SF.IdentifierName(dllName)).WithUsingKeyword(SF.Token(SyntaxKind.UsingKeyword)).WithAlias(aliasNameEqualsSyntax).WithSemicolonToken(SF.Token(SyntaxKind.SemicolonToken));
    }

    /// <summary>
    /// This will create a sinlge using statement syntax.
    /// 
    /// Example:
    ///   using System; -> from System
    ///   using System.Linq; -> from System.Linq
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    private static UsingDirectiveSyntax ComposeUsing(UsingDirectiveDefinition usingDefinition)
    {
      if (usingDefinition.IsStatic)
      {
        return StaticUsing(usingDefinition.Name.Output);
      }
      else if (usingDefinition.UseAlias)
      {
        return AliasUsing(usingDefinition.Name.Output, usingDefinition.Alias);
      }
      else
      {
        return StandardUsing(usingDefinition.Name.Output);
      }
    }

    /// <summary>
		/// Adds using statements to the syntax in the compliation unit (represents the final file)
		/// </summary>
		/// <param name="compilationUnit">The compliation unit to update</param>
		/// <param name="usings">a list of usings to create using statements with</param>
		/// <returns>the updated compilation unit</returns>
		public static CompilationUnitSyntax AddUsingDirectives(this CompilationUnitSyntax compilationUnit, IList<UsingDirectiveDefinition> usingDefinitions)
    {
      if (usingDefinitions != null && usingDefinitions.Count > 0)
      {
        var usingList = new List<UsingDirectiveSyntax>();

        foreach (var usingDefinition in usingDefinitions)
        {
          var directive = ComposeUsing(usingDefinition);

          if (directive != null)
          {
            usingList.Add(directive);
          }
        }

        return compilationUnit.WithUsings(SF.List(usingList));
      }
      else
      {
        return compilationUnit;
      }
    }

  }
}
