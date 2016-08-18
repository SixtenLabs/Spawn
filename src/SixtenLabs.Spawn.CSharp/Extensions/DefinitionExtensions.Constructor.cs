using Microsoft.CodeAnalysis.CSharp.Syntax;
using SF = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Spawn.CSharp.Extensions
{
  public static partial class DefinitionExtensions
  {
    public static ConstructorDeclarationSyntax CreateConstructorDeclaration(this ConstructorDefinition constructorDefinition)
    {
      var modifiers = GetModifierTokens(constructorDefinition.ModifierDefinitions);

      var returnTypeString = constructorDefinition.TranslatedReturnType;
      var attributes = constructorDefinition.Attributes.GetAttributeDeclarations();
      var parameters = constructorDefinition.Parameters.GetParameterDeclarations();
      var body = CreateBlock(constructorDefinition.Block);

      var constructorDeclaration = SF.ConstructorDeclaration(SF.Identifier(constructorDefinition.TranslatedName))
        .WithModifiers(modifiers);

      if (constructorDefinition.Attributes.Count > 0)
      {
        constructorDeclaration = constructorDeclaration.WithAttributeLists(SF.SingletonList(attributes));
      }

      if (constructorDefinition.Parameters.Count > 0)
      {
        constructorDeclaration = constructorDeclaration.WithParameterList(parameters);
      }

      if (constructorDefinition.HasBlock)
      {
        constructorDeclaration = constructorDeclaration.WithBody(body);
      }

      return constructorDeclaration;
    }

    public static IList<ConstructorDeclarationSyntax> GetConstructorDeclarations(this IList<ConstructorDefinition> constructorDefinitions)
    {
      var list = new List<ConstructorDeclarationSyntax>();

      foreach (var constructorDefinition in constructorDefinitions)
      {
        var constructorDeclaration = constructorDefinition.CreateConstructorDeclaration();
        list.Add(constructorDeclaration);
      }

      return list;
    }
  }
}
