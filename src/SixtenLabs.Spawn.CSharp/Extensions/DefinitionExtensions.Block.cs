using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using SF = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace SixtenLabs.Spawn.CSharp.Extensions
{
  public static partial class DefinitionExtensions
  {
    /// <summary>
		/// Currently support statements defined completely by the user.
		/// 
		/// TODO : Explore support all the different statement syntaxes (a lot of work not sure it brings any value at this point).
		/// 
		/// </summary>
		/// <param name="definition"></param>
		/// <returns></returns>
		private static BlockSyntax CreateBlock(this BlockDefinition definition)
    {
      var statementList = new List<StatementSyntax>();

      foreach (var statement in definition.StatementDefinitions)
      {
        var declaration = SF.ParseStatement(statement.Code);
        statementList.Add(declaration);
      }

      return SF.Block(statementList);
    }
  }
}
