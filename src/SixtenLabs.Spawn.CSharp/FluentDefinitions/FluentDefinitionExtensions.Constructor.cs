using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Spawn.CSharp.FluentDefinitions
{
  /// <summary>
  /// Fluent ConstructorDefinition Extensions
  /// </summary>
  public static partial class FluentDefinitionExtensions
  {
    public static ConstructorDefinition AddModifier(this ConstructorDefinition definition, SyntaxKindDto kind)
    {
      var modifierDefinition = new ModifierDefinition() { Modifier = kind };
      definition.ModifierDefinitions.Add(modifierDefinition);

      return definition;
    }

    public static ConstructorDefinition AddParameter(this ConstructorDefinition definition, string name, string parameterType)
    {
      var parameterDefinition = new ParameterDefinition(name).WithParameterType(parameterType);
      definition.Parameters.Add(parameterDefinition);

      return definition;
    }
  }
}
