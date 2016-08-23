using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Spawn.CSharp.FluentDefinitions
{
  /// <summary>
  /// Fluent ModifierDefinition Extensions
  /// </summary>
  public static partial class FluentDefinitionExtensions
  {
    public static T WithModifier<T>(this T definition, SyntaxKindDto modifier) where T : IHaveModifiers
    {
      var modifierDefinition = new ModifierDefinition() { Modifier = modifier };
      definition.ModifierDefinitions.Add(modifierDefinition);

      return definition;
    }

    public static T WithModifiers<T>(this T definition, params SyntaxKindDto[] modifiers) where T : IHaveModifiers
    {
      foreach (var modifier in modifiers)
      {
        definition.WithModifier(modifier);
      }

      return definition;
    }
  }
}
