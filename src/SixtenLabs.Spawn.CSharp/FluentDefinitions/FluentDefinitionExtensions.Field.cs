using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Spawn.CSharp.FluentDefinitions
{
  /// <summary>
  /// Fluent FieldDefinition Extensions
  /// </summary>
  public static partial class FluentDefinitionExtensions
  {
    public static FieldDefinition AddModifier(this FieldDefinition definition, SyntaxKindDto kind)
    {
      var modifierDefinition = new ModifierDefinition() { Modifier = kind };
      definition.ModifierDefinitions.Add(modifierDefinition);

      return definition;
    }

    public static FieldDefinition AddAttribute(this FieldDefinition definition, string name, params string[] arguments)
    {
      var attributeDefinition = new AttributeDefinition(name);

      foreach(var argument in arguments)
      {
        attributeDefinition.ArgumentList.Add(argument);
      }

      definition.AttributeDefinitions.Add(attributeDefinition);

      return definition;
    }

    public static FieldDefinition WithReturnType(this FieldDefinition definition, string returnType)
    {
      definition.ReturnType.OriginalName = returnType;

      return definition;
    }

    public static FieldDefinition WithDefaultValue(this FieldDefinition definition, LiteralDefinition defaultValue)
    {
      definition.DefaultValue = defaultValue;

      return definition;
    }
  }
}
