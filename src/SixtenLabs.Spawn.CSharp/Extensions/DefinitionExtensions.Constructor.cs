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

      var attributes = constructorDefinition.AttributeDefinitions.GetAttributeDeclarations();
      var parameters = constructorDefinition.ParameterDefinitions.GetParameterDeclarations();
      var body = CreateBlock(constructorDefinition.BlockDefinition);

      var constructorDeclaration = SF.ConstructorDeclaration(SF.Identifier(constructorDefinition.Name.Output))
        .WithModifiers(modifiers);

      if (constructorDefinition.AttributeDefinitions.Count > 0)
      {
        constructorDeclaration = constructorDeclaration.WithAttributeLists(SF.SingletonList(attributes));
      }

      if (constructorDefinition.ParameterDefinitions.Count > 0)
      {
        constructorDeclaration = constructorDeclaration.WithParameterList(parameters);
      }

      if (!constructorDefinition.BlockDefinition.IsEmpty)
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
