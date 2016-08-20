using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Spawn.CSharp.FluentDefinitions
{
  /// <summary>
  /// Fluent PropertyDefinition Extensions
  /// </summary>
  public static partial class FluentDefinitionExtensions
  {
    public static PropertyDefinition WithReturnType(this PropertyDefinition definition, string returnType)
    {
      definition.ReturnType.OriginalName = returnType;

      return definition;
    }

    public static PropertyDefinition WithDefaultValue(this PropertyDefinition definition, LiteralDefinition defaultValue)
    {
      definition.DefaultValue = defaultValue;

      return definition;
    }
  }
}
