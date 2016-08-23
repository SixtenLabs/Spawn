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
    public static PropertyDefinition AddProperty(this IHaveProperties parentDefinition, string name, string returnType = null, string defaultValue = null)
    {
      var propertyDefinition = new PropertyDefinition(name).WithReturnType(returnType);

      if (defaultValue != null)
      {
        var literalDefinition = new LiteralDefinition() { Value = defaultValue, LiteralType = defaultValue.GetType() };
        propertyDefinition.WithDefaultValue(literalDefinition);
      }

      parentDefinition.PropertyDefinitions.Add(propertyDefinition);

      return propertyDefinition;
    }

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
