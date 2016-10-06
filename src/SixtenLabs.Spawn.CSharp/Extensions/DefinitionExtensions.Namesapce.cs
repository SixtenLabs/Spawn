using Microsoft.CodeAnalysis.CSharp.Syntax;
using SF = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace SixtenLabs.Spawn.CSharp.Extensions
{
  public static partial class DefinitionExtensions
  {
    public static NamespaceDeclarationSyntax CreateNamespaceDeclaration(this NamespaceDefinition @namespace)
    {
      if (@namespace == null)
      {
        return null;
      }

      return SF.NamespaceDeclaration(SF.IdentifierName(@namespace.Name.Output));
    }
  }
}
