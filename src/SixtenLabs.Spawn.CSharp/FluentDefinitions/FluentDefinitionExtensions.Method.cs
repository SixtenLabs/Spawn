using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Spawn.CSharp.FluentDefinitions
{
  /// <summary>
  /// Fluent MethodDefinition Extensions
  /// </summary>
  public static partial class FluentDefinitionExtensions
  {
    public static MethodDefinition AddModifier(this MethodDefinition definition, SyntaxKindDto kind)
    {
      var modifierDefinition = new ModifierDefinition() { Modifier = kind };
      definition.ModifierDefinitions.Add(modifierDefinition);

      return definition;
    }
  }
}
